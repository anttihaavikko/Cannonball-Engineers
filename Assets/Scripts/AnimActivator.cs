using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimActivator : MonoBehaviour
{
    public Launcher launcher;

    public void Launch()
    {
        launcher.Launch(new Vector3(25f, 40f, 0f));
    }
}
