using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToTitle : MonoBehaviour
{

    private bool isFadeNow = false;
    
    // Update is called once per frame
    async void Update()
    {
        if (!isFadeNow && ControllerManager.instance.CtrlInput.Player.Select.WasPressedThisFrame())
        {
            isFadeNow = true;
            await SceneFade.instance.SceneChange("TitleScene");
        }
    }
}
