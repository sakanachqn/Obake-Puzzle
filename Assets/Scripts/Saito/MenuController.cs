using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuController : MonoBehaviour
{
    public GameObject Black;
     

    
    void Update()
    {
        if (ControllerManager.instance.CtrlInput.Menu.OpenMenu.WasPerformedThisFrame())
        {
            
        }
    }
}

