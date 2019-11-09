using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public HingeJoint2D joint;
    public Dude dude;

    private bool hasGrabbed;
    private bool canGrab = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!dude.isAlive || hasGrabbed || !dude.canGrab || !canGrab) return;

        if (collision.otherCollider.tag != "Grabber") return;

        if(collision.gameObject.tag == "Wall" || CanGrab(collision.gameObject))
        {
            joint.enabled = true;
            joint.connectedBody = collision.rigidbody;
            joint.anchor = transform.InverseTransformPoint(collision.contacts[0].point);
            hasGrabbed = true;

            dude.UnFollow();
            dude.Attach();

            EffectManager.Instance.AddEffect(2, collision.contacts[0].point);
        }
    }

    bool CanGrab(GameObject go)
    {
        if(go.tag == "Block")
        {
            Block block = go.GetComponent<Block>();
            if(!dude.HasActivated(block))
            {
                dude.ActivateBlock(block);
                block.Activate();
            }
            return true;
        }

        return go.tag == "Limb" && go.transform.parent != transform.parent;
    }

    public void Detach()
	{
        canGrab = false;
        dude.canDie = false;
        joint.enabled = false;
		hasGrabbed = false;

        Invoke("EnableGrab", 0.2f);
	}

    void EnableGrab()
    {
        canGrab = true;
        dude.canDie = !dude.hardHat;
    }
}
