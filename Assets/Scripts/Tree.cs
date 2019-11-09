using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tree : MonoBehaviour
{

    public List<GameObject> branches;

    // Start is called before the first frame update
    void Start()
    {
        var scl = Random.Range(0.9f, 1.1f) * transform.localScale.x;
        transform.localScale = new Vector3(Random.value < 0.5f ? scl : -scl, scl, 1f);
        branches.ForEach(b => b.SetActive(Random.value < 0.7f));
    }
}
