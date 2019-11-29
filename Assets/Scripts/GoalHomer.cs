using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalHomer : MonoBehaviour
{
    public Rigidbody2D body;
    public Dude dude;
	public bool isIntro;

    private bool homing;
    private Vector3 goalPos;


    // Update is called once per frame
    void Update()
    {
        if (homing)
        {
            var diff = goalPos - body.transform.position;
            body.AddForce(diff * 750f, ForceMode2D.Force);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!homing && collision.gameObject.tag == "Goal" && dude.isAlive && !dude.IsAttachedOrGrabbed())
        {
            dude.canDie = false;
            dude.canGrab = false;
            homing = true;
            goalPos = collision.gameObject.transform.position;
            body.gravityScale = 0f;

			Invoke("DoSuccess", 2f);
            GameManager.Instance.running = false;

            AudioManager.Instance.PlayEffectAt(21, transform.position, 1.48f);
            AudioManager.Instance.PlayEffectAt(27, transform.position, 0.259f);
            AudioManager.Instance.PlayEffectAt(39, transform.position, 1.205f);
            AudioManager.Instance.PlayEffectAt(33, transform.position, 1.444f);

            AudioManager.Instance.PlayEffectAt(Random.Range(63, 70), transform.position, 2f);

            if(!isIntro)
                AudioManager.Instance.Lowpass();

			if (isIntro)
				Invoke("ToStart", 3f);
        }
    }

    void DoSuccess()
	{
        GameManager.Instance.Success();
	}

    void ToStart()
	{
        SceneChanger.Instance.ChangeScene("Start");
	}
}
