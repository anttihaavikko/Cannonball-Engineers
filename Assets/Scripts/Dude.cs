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

    // Start is called before the first frame update
    void Start()
    {
        activatedBlocks = new List<Block>();
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
        isAlive = false;

        if (activatedBlocks.Any())
        {
            activatedBlocks.ForEach(b => b.Deactivate());
        }

        EffectManager.Instance.AddEffect(0, body.transform.position);
        EffectManager.Instance.AddEffect(1, body.transform.position);

        GetComponentsInChildren<Grabber>().ToList().ForEach(g => g.enabled = false);
        GetComponentsInChildren<HingeJoint2D>().ToList().ForEach(j => j.enabled = false);
        GetComponentsInChildren<Rigidbody2D>().ToList().ForEach(rb => rb.gameObject.tag = "BodyPart");
    }
}
