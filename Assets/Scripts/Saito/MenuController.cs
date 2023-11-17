using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject Image;

    
    void Update()
    {
        if (ControllerManager.instance.CtrlInput.Menu.OpenMenu.WasPerformedThisFrame())
        {
            Image.SetActive(true);
        }
    }
}
