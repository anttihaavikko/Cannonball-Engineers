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
            collision.gameObject.tag == "MultiBlock" ||
            collision.gameObject.tag == "Slippery")
        {
            if(collision.relativeVelocity.magnitude > 30 && !dude.hardHat)
            {
                dude.Die();

                if (!Manager.Instance.hasBashedHead)
                {
                    Manager.Instance.hasBashedHead = true;
                    TutorialDude.Instance.Show("Head is a very vunerable spot unless it is protected with a helmet!", 0.5f);
                }

            } else

            if (collision.relativeVelocity.magnitude > 5f && !dude.hardHat)
            {
                AudioManager.Instance.PlayEffectAt(34, transform.position, 1f);
                AudioManager.Instance.PlayEffectAt(29, transform.position, 1f);
            }
        }
    }
}
