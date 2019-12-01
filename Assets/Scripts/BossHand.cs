using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHand : MonoBehaviour
{
    public Rigidbody2D body;
    public bool isWorking = true;

    private Dude target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = body.transform.position;

        if(target && target.isAlive && isWorking)
        {
            var dir = transform.position - target.body.transform.position;
            body.AddForce(dir.normalized * -1000f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Limb")
        {
            target = collision.gameObject.GetComponentInParent<Dude>();
        }
    }
}
