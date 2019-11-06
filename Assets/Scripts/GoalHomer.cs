using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalHomer : MonoBehaviour
{
    public Rigidbody2D body;
    public Dude dude;

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
        if (!homing && collision.gameObject.tag == "Goal")
        {
            dude.canDie = false;
            dude.canGrab = false;
            homing = true;
            goalPos = collision.gameObject.transform.position;
            body.gravityScale = 0f;
        }
    }
}
