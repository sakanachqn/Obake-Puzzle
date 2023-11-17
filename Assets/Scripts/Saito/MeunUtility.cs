using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeunUtility : MonoBehaviour
{
    public GameObject Image;

    void Update()
    {
        if (ControllerManager.instance.CtrlInput.Menu.OpenMenu.WasPerformedThisFrame())
        {
            Image.SetActive(false);
        }
        else if (ControllerManager.instance.CtrlInput.Menu.PushABotton.WasPerformedThisFrame())
        {
            //今後シーンが出来たらここに書く
        }
        else if (ControllerManager.instance.CtrlInput.Menu.PushBBotton.WasPerformedThisFrame())
        {
            //今後シーンが出来たらここに書く
        }
    }
}
