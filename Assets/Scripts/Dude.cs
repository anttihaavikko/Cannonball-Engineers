using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Dude : MonoBehaviour
{
    public Rigidbody2D body;
    public bool isAlive = true;
    public LineRenderer line;
    public bool canDie = true;
    public bool canGrab = true;
    public Rigidbody2D[] hands;
    public Rigidbody2D[] limbs;
    public List<Grabber> grabbers;
    public bool jumper;
    public bool hardHat;
    public Launcher launcher;
    public List<GameObject> outlines;
    public GameObject hat;
    public GameObject clicker;
    public GoalHomer goalHomer;

    private List<Block> activatedBlocks;
    private List<HingeJoint2D> joints;
    private List<Rigidbody2D> bodies;
    private Material lineMaterial;
    private Cinemachine.CinemachineVirtualCamera followCam;
    private EffectCamera cam;
    private bool firstJump = true;
    private bool isAttached;
    private bool isGrabbed;
    private bool bounced;

    // Start is called before the first frame update
    void Start()
    {
        activatedBlocks = new List<Block>();

        joints = GetComponentsInChildren<HingeJoint2D>().ToList();
        bodies = GetComponentsInChildren<Rigidbody2D>().ToList();

        lineMaterial = line.material;

        cam = Camera.main.GetComponent<EffectCamera>();
    }

    public void AddHardHat(bool h)
    {
        hardHat = h;
        hat.SetActive(h);
    }

    private void Update()
    {
        joints.FindAll(j => j.enabled).ForEach(j =>
        {
            if (!j.connectedBody) return;

            var p1 = j.transform.TransformPoint(j.anchor);
            var p2 = j.connectedBody.transform.TransformPoint(j.connectedAnchor);
            var diff = (p1 - p2).magnitude;

            if (diff > 1)
            {
                //AudioManager.Instance.PlayEffectAt(89, p1, 4f);
                Die();

                if(!Manager.Instance.hasSeenSnap)
                {
                    Manager.Instance.hasSeenSnap = true;
                    TutorialDude.Instance.Show("Try to not rip their limbs apart!", 0.5f);
                }
            }
        });
    }

    public void UpdateLine()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        line.SetPosition(0, body.position);
        line.SetPosition(1, pos);
    }

    public bool IsDone()
    {
        return !jumper && isAttached && !isGrabbed;
    }

    public bool CanBeActivated()
    {
        if (this == Manager.Instance.activeDude) return false;

        return (jumper || firstJump) && isAlive && (firstJump || isAttached || isGrabbed);
    }

    public void Attach()
    {
        if (isAttached) return;

        //launcher.AddDude();

        isAttached = true;

        EndTurn();
    }

    bool IsActiveOne()
    {
        return this != Manager.Instance.activeDude;
    }

    void EndTurn()
    {
        if (jumper)
        {
            Manager.Instance.activeDude.line.enabled = true;
        }
        else
            launcher.ActivateReserve();
    }

    // Update is called once per frame
    public bool Launch(Cinemachine.CinemachineVirtualCamera fCam, Vector3 dir, bool manualTorque = false, float torqueAmount = 0f)
    {
        if (!isAlive)
            return false;

        if (!firstJump && !jumper)
            return false;

        if (!firstJump && !isAttached && !isGrabbed)
            return false;

        isAttached = false;

        activatedBlocks.ForEach(b => b.Deactivate());
        activatedBlocks.Clear();

        grabbers.ForEach(g => g.Detach());        

        followCam = fCam;
        followCam.gameObject.SetActive(true);
        followCam.Follow = body.transform;

        body.bodyType = RigidbodyType2D.Dynamic;
        limbs.ToList().ForEach(l => l.bodyType = RigidbodyType2D.Dynamic);

        line.enabled = false;

        body.AddForce(dir * 150f, ForceMode2D.Impulse);

        if (activatedBlocks.Any())
        {
            activatedBlocks.ForEach(b => b.Deactivate());
        }

        if (firstJump)
        {
            var amt = manualTorque ? torqueAmount * 3000f : dir.x * 100f;
            body.AddTorque(amt, ForceMode2D.Impulse);
            cam.BaseEffect(1.1f);

            AudioManager.Instance.PlayEffectAt(32, transform.position, 2f);
            AudioManager.Instance.PlayEffectAt(29, transform.position, 0.489f);
            AudioManager.Instance.PlayEffectAt(47, transform.position, 0.852f);
            AudioManager.Instance.PlayEffectAt(49, transform.position, 1f);
        }

        AudioManager.Instance.PlayEffectAt(31, transform.position, 0.723f);
        AudioManager.Instance.PlayEffectAt(25, transform.position, 1.104f);
        AudioManager.Instance.PlayEffectAt(37, transform.position, 1.241f);
        AudioManager.Instance.PlayEffectAt(40, transform.position, 1f);
        AudioManager.Instance.PlayEffectAt(42, transform.position, 0.643f);

        hands.ToList().ForEach(h => h.AddForce(dir * Random.Range(5f, 10f), ForceMode2D.Impulse));

        var ret = firstJump;

        firstJump = false;

        return ret;
    }

    public void ActivateBlock(Block block)
    {
        activatedBlocks.Add(block);
    }

    public bool HasActivated(Block block)
    {
        return activatedBlocks.Contains(block);
    }

    public void UnFollow()
    {
        if(followCam && followCam.gameObject)
            followCam.gameObject.SetActive(false);
    }

    void Nudge()
    {
        var dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        body.AddForce(dir * 500f, ForceMode2D.Impulse);
        Invoke("Nudge", 1f);
    }

    public void NudgeHands()
    {
        hands.ToList().ForEach(h =>
        {
            var dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            h.AddForce(dir * 100f, ForceMode2D.Impulse);
        });
    }

    public void Die()
    {
        if (!isAlive || !canDie) return;

        if (!Manager.Instance.hasDoneTorque && Manager.Instance.levelToActivate > 0)
        {
            TutorialDude.Instance.Show("You can also manually control the launch torque from the meter under the cannon.", 0.5f);
        }

        AudioManager.Instance.curMusic.pitch = 0.85f;

        AudioManager.Instance.PlayEffectAt(1, body.transform.position, 1.285f);
        AudioManager.Instance.PlayEffectAt(4, body.transform.position, 1.653f);
        AudioManager.Instance.PlayEffectAt(6, body.transform.position, 1.828f);
        AudioManager.Instance.PlayEffectAt(10, body.transform.position, 1.4f);
        AudioManager.Instance.PlayEffectAt(18, body.transform.position, 0.876f);

        AudioManager.Instance.PlayEffectAt(Random.Range(70, 78), body.transform.position, 1.5f);

        GameManager.Instance.deaths++;

        clicker.SetActive(false);

        ToggleOutline(false);

        launcher.ActivateIfNeeded();

        line.enabled = false;

        cam.BaseEffect(2f);

        isAlive = false;

        UnFollow();

        if (activatedBlocks.Any())
        {
            activatedBlocks.ForEach(b => b.Deactivate());
        }

        EffectManager.Instance.AddEffect(0, body.transform.position);
        EffectManager.Instance.AddEffect(1, body.transform.position);
        EffectManager.Instance.AddEffect(6, body.transform.position);

        grabbers.ForEach(g =>
        {
            g.enabled = false;
            g.DetachMultiBlock();
        });
        joints.ForEach(j => j.enabled = false);
        bodies.ForEach(rb =>
        {
            //rb.gameObject.tag = "BodyPart";
            var dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            rb.AddForce(dir * rb.mass * 100f, ForceMode2D.Impulse);
        });
    }

    public void ToggleOutline(bool state)
    {
        if (!firstJump && !isAttached || !isAlive) return;

        outlines.ForEach(o => o.SetActive(state));
    }

    public bool IsAttachedOrGrabbed()
    {
        return isAttached || isGrabbed;
    }

    public void GetGrabbed()
    {
        isGrabbed = true;
        EndTurn();
    }

    public void Bounce()
    {
        canDie = false;
        CancelInvoke("AfterBounce");
        Invoke("AfterBounce", 0.3f);
    }

    void AfterBounce()
    {
        canDie = true;
    }
}
