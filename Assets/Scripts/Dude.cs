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

    private List<Block> activatedBlocks;
    private List<Grabber> grabbers;
    private List<HingeJoint2D> joints;
    private List<Rigidbody2D> bodies;
    private Material lineMaterial;
    private Cinemachine.CinemachineVirtualCamera followCam;

    // Start is called before the first frame update
    void Start()
    {
        activatedBlocks = new List<Block>();

        grabbers = GetComponentsInChildren<Grabber>().ToList();
        joints = GetComponentsInChildren<HingeJoint2D>().ToList();
        bodies = GetComponentsInChildren<Rigidbody2D>().ToList();

        lineMaterial = line.material;
    }

    private void Update()
    {
        joints.ForEach(j =>
        {
            if (!j.connectedBody) return;

            var p1 = j.transform.TransformPoint(j.anchor);
            var p2 = j.connectedBody.transform.TransformPoint(j.connectedAnchor);
            var diff = (p1 - p2).magnitude;

            if (diff > 1) Die();
        });
    }

    public void UpdateLine()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        line.SetPosition(0, body.position);
        line.SetPosition(1, pos);

        //lineMaterial.SetTextureScale("_MainTex", new Vector2((body.transform.position - pos).magnitude * 0.1f, 0.5f));
    }

    // Update is called once per frame
    public void Launch(Cinemachine.CinemachineVirtualCamera cam)
    {
        followCam = cam;
        followCam.gameObject.SetActive(true);
        followCam.Follow = body.transform;

        body.bodyType = RigidbodyType2D.Dynamic;

        line.enabled = false;

        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var dir = pos - body.transform.position;

        body.AddForce(dir * 150f, ForceMode2D.Impulse);

        hands.ToList().ForEach(h => h.AddForce(dir * Random.Range(5f, 10f), ForceMode2D.Impulse));
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
        followCam.gameObject.SetActive(false);

        //if(isAlive)
        //    Invoke("Nudge", 1f);
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

        isAlive = false;

        UnFollow();

        if (activatedBlocks.Any())
        {
            activatedBlocks.ForEach(b => b.Deactivate());
        }

        EffectManager.Instance.AddEffect(0, body.transform.position);
        EffectManager.Instance.AddEffect(1, body.transform.position);

        grabbers.ForEach(g => g.enabled = false);
        joints.ForEach(j => j.enabled = false);
        bodies.ForEach(rb =>
        {
            rb.gameObject.tag = "BodyPart";
            var dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            rb.AddForce(dir * rb.mass * 100f, ForceMode2D.Impulse);
        });
    }
}
