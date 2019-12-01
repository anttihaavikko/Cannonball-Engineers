using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Block : MonoBehaviour
{
    public Vector3 direction;
    public Rigidbody2D body;
    public SpriteRenderer[] icons;
    public Door door;
    public List<Door> moreDoors;
    public GameObject wire;
    public List<Gear> gears;
    public int angle;
    public float moveTime = 1.5f;
    public bool multipleActivations;
    public bool eachActivates;
    public bool isActivator;
    public bool isBoss;
    public List<BossHand> deactivatesHands;

    private Vector3 startPos;
    private int activations;

    private void Start()
    {
        startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isActivator && collision.gameObject.tag == "ActivationArea")
        {
            Activate(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isActivator && collision.gameObject.tag == "ActivationArea")
        {
            Deactivate(true);
        }
    }

    public void Activate(bool forced = false)
    {
        if (isActivator && !forced) return;

        activations++;

        var lightNum = 0;
        icons.ToList().ForEach(i =>
        {

            if(!eachActivates || lightNum < activations)
            {
                i.color = Color.white;
                EffectManager.Instance.AddEffect(3, i.transform.position);
            }

            lightNum++;
        });

        AudioManager.Instance.PlayEffectAt(2, transform.position, 1f);
        AudioManager.Instance.PlayEffectAt(3, transform.position, 1f);
        AudioManager.Instance.PlayEffectAt(5, transform.position, 1f);
        AudioManager.Instance.PlayEffectAt(0, transform.position, 1f);
        AudioManager.Instance.PlayEffectAt(14, transform.position, 1f);

        deactivatesHands.ForEach(h => h.isWorking = false);

        if (wire)
            wire.SetActive(true);

        float multi = multipleActivations || eachActivates ? activations : 1f;
        gears.ForEach(g => Tweener.Instance.RotateTo(g.transform, Quaternion.Euler(0, 0, g.amount * multi), moveTime, 0f, TweenEasings.LinearInterpolation));

        if (door)
        {
            door.Open(moveTime);
            return;
        }

        if(moreDoors.Any())
        {
            moreDoors.ForEach(d => d.Open(moveTime));
        }

        if(angle != 0)
        {
            Invoke("DoRotation", 0.25f);
            return;
        }

        Tweener.Instance.MoveBodyTo(body, startPos + direction * multi, moveTime, 0f, TweenEasings.LinearInterpolation);
        DoSounds(moveTime);

        if(isBoss && activations >= 7) {
            GameManager.Instance.running = false;

            AudioManager.Instance.PlayEffectAt(21, transform.position, 1.48f);
            AudioManager.Instance.PlayEffectAt(27, transform.position, 0.259f);
            AudioManager.Instance.PlayEffectAt(39, transform.position, 1.205f);
            AudioManager.Instance.PlayEffectAt(33, transform.position, 1.444f);

            AudioManager.Instance.PlayEffectAt(Random.Range(63, 70), transform.position, 2f);

            AudioManager.Instance.ChangeMusic(1, 0.25f, 0.25f, 0f);

            Invoke("DoSuccess", 1f);
        }
    }

    void DoSuccess()
    {
        GameManager.Instance.Success();
    }

    public void Deactivate(bool forced = false)
    {
        if (isActivator && !forced) return;

        activations--;

        icons.ToList().ForEach(i => i.color = new Color(0.25f, 0.25f, 0.25f));

        AudioManager.Instance.PlayEffectAt(13, transform.position, 1f);
        AudioManager.Instance.PlayEffectAt(14, transform.position, 0.226f);
        AudioManager.Instance.PlayEffectAt(23, transform.position, 0.729f);

        if (wire)
            wire.SetActive(false);

        deactivatesHands.ForEach(h => h.isWorking = true);

        float rotMulti = multipleActivations ? activations : 0f;
        gears.ForEach(g => Tweener.Instance.RotateTo(g.transform, Quaternion.Euler(0, 0, g.amount * rotMulti), moveTime, 0f, TweenEasings.LinearInterpolation));

        if (door)
        {
            door.Close(moveTime * 0.5f);
            return;
        }

        if (moreDoors.Any())
        {
            moreDoors.ForEach(d => d.Close(moveTime * 0.5f));
        }

        if (angle != 0)
        {
            Invoke("DoRotation", 0.25f);
            return;
        }

        Tweener.Instance.MoveBodyTo(body, startPos, moveTime, 0f, TweenEasings.LinearInterpolation);
        DoSounds(moveTime);
    }

    void DoRotation()
    {
        Tweener.Instance.RotateBodyTo(body, Quaternion.Euler(0, 0, angle * activations), moveTime - 0.25f, 0f, TweenEasings.LinearInterpolation);
        DoSounds(moveTime - 0.25f);
    }

    void DoSounds(float duration)
    {
        CancelInvoke("Clang");
        CancelInvoke("DoSound");

        Invoke("Clang", duration);

        for (float d = 0f; d < duration - 1f; d += 1.3f)
            Invoke("DoSound", d);
    }

    void DoSound()
    {
        AudioManager.Instance.PlayEffectAt(23, transform.position, 0.747f);
        AudioManager.Instance.PlayEffectAt(24, transform.position, 0.796f);
        AudioManager.Instance.PlayEffectAt(37, transform.position, 1f);
    }

    void Clang()
    {
        AudioManager.Instance.PlayEffectAt(1, transform.position, 0.579f);
        AudioManager.Instance.PlayEffectAt(11, transform.position, 1f);
        AudioManager.Instance.PlayEffectAt(17, transform.position, 0.705f);
        AudioManager.Instance.PlayEffectAt(16, transform.position, 0.587f);
        AudioManager.Instance.PlayEffectAt(39, transform.position, 1.12f);
    }
}
