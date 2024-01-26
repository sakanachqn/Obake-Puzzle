using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeunUtility : MonoBehaviour
{

    Button Retry;
    Button Select;
    Button Close;

    [SerializeField]
    private List<Button> buttonList;
    ControllerInput CtrlInput;
    float selectF;

    void Start()
    {
        // ボタンコンポーネントの取得
        Retry = GameObject.Find("/Canvas/Black/Menu/Retry").GetComponent<Button>();
        Select = GameObject.Find("/Canvas/Black/Menu/Select").GetComponent<Button>();
        Close = GameObject.Find("/Canvas/Black/Menu/Close").GetComponent<Button>();
        CtrlInput = ControllerManager.instance.CtrlInput;


        // 最初に選択状態にしたいボタンの設定
        Retry.Select();

    }

    public void SelectRetry()
    {
        Retry.Select();
    }

    public void SelectSelect()
    {
        Select.Select();
    }

    public void SelectClose()
    {
        Close.Select();
    }


    private void Update()
    {
        Vector2 menuinput = CtrlInput.Menu.MenuSelect.ReadValue<Vector2>();

        if (menuinput.y > -0.05f && menuinput.y < 0.05f) menuinput.y = 0f;

        selectF += menuinput.y * Time.deltaTime;

        if (selectF <= 0f) selectF = 0f;
        if (selectF >= 3f) selectF = 2.999999f;

        buttonList[(int)selectF].Select();
    }

}