using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndView : MonoBehaviour
{

    private void Start()
    {
        SceneChanger.Instance.AttachCamera();
        Manager.Instance.isHoveringSomething = false;
    }
}
