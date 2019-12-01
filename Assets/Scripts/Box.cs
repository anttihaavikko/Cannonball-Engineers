using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > 2f && collision.gameObject.tag != "Grabber")
        {
            float vol = collision.relativeVelocity.magnitude * 0.02f;
            AudioManager.Instance.PlayEffectAt(10, transform.position, 0.73f * vol);
            AudioManager.Instance.PlayEffectAt(9, transform.position, 1f * vol);
            AudioManager.Instance.PlayEffectAt(3, transform.position, 0.844f * vol);
            AudioManager.Instance.PlayEffectAt(47, transform.position, 1.195f * vol);
        }
    }
}
