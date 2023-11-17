using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class ControllStart : MonoBehaviour
{
    [SerializeField]
    private SceneFade _sceneFade;
    private bool _isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPlaying) return;
        if (ControllerManager.instance.CtrlInput.Menu.PushBBotton.WasPerformedThisFrame())//Bボタンを押したときの処理
        {
            _sceneFade.SceneChange("TestPlay").Forget();
            _isPlaying = true;
        }
    }
}
