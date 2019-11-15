using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var rb = collision.rigidbody;
        var dude = collision.gameObject.GetComponentInParent<Dude>();

        if(rb && dude)
        {
            dude.Bounce();
        }
    }
}
