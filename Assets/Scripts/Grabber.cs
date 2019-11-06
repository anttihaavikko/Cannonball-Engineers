using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public HingeJoint2D joint;
    public Dude dude;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!dude.isAlive) return;

        if (collision.otherCollider.tag != "Grabber") return;

        if(collision.gameObject.tag == "Wall" || CanGrab(collision.gameObject))
        {
            joint.enabled = true;
            joint.connectedBody = collision.rigidbody;
            joint.anchor = transform.InverseTransformPoint(collision.contacts[0].point);
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
}
