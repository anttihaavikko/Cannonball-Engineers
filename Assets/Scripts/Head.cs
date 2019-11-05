using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public Dude dude;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            if(collision.relativeVelocity.magnitude > 30)
            {
                Debug.Log(collision.relativeVelocity.magnitude);
                dude.Die();
            }
        }
    }
}
