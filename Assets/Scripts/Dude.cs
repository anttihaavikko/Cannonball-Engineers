using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Dude : MonoBehaviour
{
    public Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Launch()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var dir = pos - body.transform.position;

        body.AddForce(dir * 150f, ForceMode2D.Impulse);
    }

    public void Die()
    {
        EffectManager.Instance.AddEffect(0, body.transform.position);
        EffectManager.Instance.AddEffect(1, body.transform.position);

        GetComponentsInChildren<Grabber>().ToList().ForEach(g => g.enabled = false);
        GetComponentsInChildren<HingeJoint2D>().ToList().ForEach(j => j.enabled = false);
        GetComponentsInChildren<Rigidbody2D>().ToList().ForEach(rb => rb.gameObject.tag = "BodyPart");
    }
}
