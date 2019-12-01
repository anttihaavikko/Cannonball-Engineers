using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelActivator : MonoBehaviour
{
    public List<GameObject> levels;

    // Start is called before the first frame update
    void Start()
    {
        if (Manager.levels.Length != levels.Count)
            Debug.LogWarning("Level counts do not match!");

        if (Manager.Instance.levelToActivate == -1) return;

        levels.ForEach(l => l.SetActive(false));
        levels[Manager.Instance.levelToActivate].SetActive(true);
    }
}
