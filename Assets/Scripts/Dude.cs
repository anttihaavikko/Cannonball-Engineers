using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Dude : MonoBehaviour
{
    public Rigidbody2D body;
    public bool isAlive = true;

    private List<Block> activatedBlocks;
    private List<Grabber> grabbers;
    private List<HingeJoint2D> joints;
    private List<Rigidbody2D> bodies;

    // Start is called before the first frame update
    void Start()
    {
        activatedBlocks = new List<Block>();

        grabbers = GetComponentsInChildren<Grabber>().ToList();
        joints = GetComponentsInChildren<HingeJoint2D>().ToList();
        bodies = GetComponentsInChildren<Rigidbody2D>().ToList();
    }

    private void Update()
    {
        joints.ForEach(j =>
        {
            if (!j.connectedBody) return;

            var p1 = j.transform.TransformPoint(j.anchor);
            var p2 = j.connectedBody.transform.TransformPoint(j.connectedAnchor);
            var diff = (p1 - p2).magnitude;

            if (diff > 1) Die();
        });
    }

    // Update is called once per frame
    public void Launch()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var dir = pos - body.transform.position;

        body.AddForce(dir * 150f, ForceMode2D.Impulse);
    }

    public void ActivateBlock(Block block)
    {
        activatedBlocks.Add(block);
    }

    public bool HasActivated(Block block)
    {
        return activatedBlocks.Contains(block);
    }

    public void Die()
    {
        if (!isAlive) return;

        isAlive = false;

        if (activatedBlocks.Any())
        {
            activatedBlocks.ForEach(b => b.Deactivate());
        }

        EffectManager.Instance.AddEffect(0, body.transform.position);
        EffectManager.Instance.AddEffect(1, body.transform.position);

        grabbers.ForEach(g => g.enabled = false);
        joints.ForEach(j => j.enabled = false);
        bodies.ForEach(rb =>
        {
            rb.gameObject.tag = "BodyPart";
            var dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            rb.AddForce(dir * rb.mass * 100f, ForceMode2D.Impulse);
        });
    }
}
