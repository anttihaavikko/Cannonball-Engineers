using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCheck : MonoBehaviour
{
    public Dude dude;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var t = collision.gameObject.tag;

        if (t == "Wall" || t == "Block" || t == "MultiBlock" || t == "Slippery")
        {
            Invoke("Die", 0.2f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var t = collision.gameObject.tag;

        if (t == "Wall" || t == "Block" || t == "MultiBlock" || t == "Slippery")
        {
            CancelInvoke("Die");
        }
    }

    private void Die()
    {
        dude.Die();

        if (!Manager.Instance.hasOverlapped)
        {
            Manager.Instance.hasOverlapped = true;
            TutorialDude.Instance.Show("Aw man, that must have hurt like hell. Try not to crush them!", 0.5f);
        }
    }
}
