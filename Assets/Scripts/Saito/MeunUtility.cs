using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class MeunUtility : MonoBehaviour
{

    public static MeunUtility Instance;

    bool isMenuOpen = false;
    
    [SerializeField]
    private List<GameObject> buttonList;
    ControllerInput CtrlInput;
    float selectCDtime = 0;
    int menuNumber = 0;
    RectTransform rtf;
    [SerializeField]
    float moveTime = 2f;
    [SerializeField]
    GameObject menuPanel;
    [SerializeField]
    GameObject menuBG;

    void Start()
    {
       if(CtrlInput == null)
        {
            CtrlInput = ControllerManager.instance.CtrlInput;
        }

        Instance = this;

       rtf = menuPanel.GetComponent<RectTransform>();

    }

    void SelectRetry()
    {
        CSVMapGenerate.Instance.Regenerate();
        Debug.Log("Retry");
    }

    async void SelectStageSelect()
    {
        await SceneFade.instance.SceneChange("StageSelect");
        Debug.Log("Select");
    }

    async void SelectClose()
    {
        await rtf.DOAnchorPosY(-1080, moveTime);
        menuBG.gameObject.SetActive(false);
        isMenuOpen = false;
        TimeCount.instance.IsTimerStop = false;
        Debug.Log("Close");
    }

    void SelectButton()
    {
        if(CtrlInput.Menu.PushABotton.WasPerformedThisFrame())
        {
            switch(menuNumber)
            {
                case 0:
                    {
                        SelectRetry();
                        break;
                    }
                case 1:
                    {
                        SelectStageSelect();
                        break;

                    }
                case 2:
                    {
                        SelectClose();
                        break;
                    }
                    default:break;
            }
            ControllerManager.instance.EnablePLInput();
        }
    }

    private void Update()
    {

        if (!isMenuOpen)
        {
            MenuOpen();
            return;
        }
        MenuSelectChange();
        SelectButton();

    }

    async void MenuOpen()
    {
        if(CtrlInput.Menu.OpenMenu.WasPerformedThisFrame())
        {
            TimeCount.instance.IsTimerStop = true;
            ControllerManager.instance.DisablePLInput();
            menuBG.SetActive(true);
            await rtf.DOAnchorPosY(0, moveTime);
            isMenuOpen = true;
        }
    }


    private void MenuSelectChange()
    {
        if(Time.time < selectCDtime + 0.2)
        {
            return;
        }
       
        Vector2 menuinput = CtrlInput.Menu.MenuSelect.ReadValue<Vector2>();

        if (menuinput.y > -0.05f && menuinput.y < 0.05f) menuinput.y = 0f;


        if (menuinput.y > 0 && menuNumber != 0)
        {
            DisableIcon(menuNumber);
            menuNumber--;
            EnableIcon(menuNumber);
           selectCDtime = Time.time;
        }

        else if (menuinput.y < 0 && menuNumber != 2)
        {
            DisableIcon(menuNumber);
            menuNumber++;
            EnableIcon(menuNumber);
            selectCDtime = Time.time;
        }

       

        //buttonList[(int)selectF].Select();
    }

    void EnableIcon(int namber)
    {
        buttonList[namber].GetComponent<MeunUtility_Image>().OnImage();
    }

    void DisableIcon(int number)
    {
        buttonList[number].GetComponent<MeunUtility_Image>().OffImage();
    }
}

