using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCheck : MonoBehaviour
{
    public Dude dude;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var t = collision.gameObject.tag;

        if (t == "Wall" || t == "Block")
        {
            Invoke("Die", 0.2f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var t = collision.gameObject.tag;

        if (t == "Wall" || t == "Block")
        {
            CancelInvoke("Die");
        }
    }

    private void Die()
    {
        dude.Die();
    }
}
