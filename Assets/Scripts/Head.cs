using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public Dude dude;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall" ||
            collision.gameObject.tag == "Block" ||
            collision.gameObject.tag == "MultiBlock")
        {
            if(collision.relativeVelocity.magnitude > 30 && !dude.hardHat)
            {
                dude.Die();
            }
        }
    }
}
