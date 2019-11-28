using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public HingeJoint2D joint;
    public Dude dude;

    private bool hasGrabbed;
    private bool canGrab = true;
    private MultiBlock multiBlock;
    private string grabDir;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!dude.isAlive || hasGrabbed || !dude.canGrab || !canGrab) return;

        if (collision.otherCollider.tag != "Grabber") return;

        if(collision.gameObject.tag == "Wall" || CanGrab(collision.gameObject, collision.collider))
        {
            float vol = 0.7f;
            AudioManager.Instance.PlayEffectAt(12, transform.position, vol * 1.268f);
            AudioManager.Instance.PlayEffectAt(13, transform.position, vol * 1.277f);
            AudioManager.Instance.PlayEffectAt(15, transform.position, vol * 0.551f);
            AudioManager.Instance.PlayEffectAt(45, transform.position, vol * 1.13f);
            AudioManager.Instance.PlayEffectAt(28, transform.position, vol * 1f);

            joint.enabled = true;
            joint.connectedBody = collision.rigidbody;
            joint.anchor = transform.InverseTransformPoint(collision.contacts[0].point);
            hasGrabbed = true;

            dude.UnFollow();
            dude.Attach();

            EffectManager.Instance.AddEffect(2, collision.contacts[0].point);

            if(collision.relativeVelocity.magnitude > 10f)
            {
                AudioManager.Instance.PlayEffectAt(Random.Range(70, 78), transform.position, 1f);
            }
        }
    }

    bool CanGrab(GameObject go, Collider2D col)
    {
        if (go.tag == "MultiBlock")
        {
            multiBlock = go.GetComponentInChildren<MultiBlock>();
            grabDir = col.name;
            multiBlock.Activate(col.name);
            return true;
        }

        if (go.tag == "Block" || go.tag == "Slippery" && dude.IsAttachedOrGrabbed())
        {
            Block block = go.GetComponent<Block>();
            if(!dude.HasActivated(block))
            {
                dude.ActivateBlock(block);
                block.Activate();
            }
            return true;
        }

        if(go.tag == "Limb" && go.transform.parent != transform.parent)
        {
            var d = go.GetComponentInParent<Dude>();
            if(d && d.isAlive)
            {
                d.GetGrabbed();
            }

            return true;
        }

        return false;
    }

    public void Detach()
	{
        canGrab = false;
        dude.canDie = false;
        joint.enabled = false;
		hasGrabbed = false;

        Invoke("EnableGrab", 0.2f);
	}

    public void DetachMultiBlock()
    {
        if (multiBlock)
            multiBlock.Deactivate(grabDir);
    }

    void EnableGrab()
    {
        canGrab = true;
        dude.canDie = true;
    }
}
