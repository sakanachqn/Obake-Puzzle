using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectController : MonoBehaviour
{
    private ControllerInput inp;

    // 今どのステージを選んでいるか
    private int nowSelectStage = 0;

    // 今どのイメージににカーソルがあっているか
    private int nowCursorPos = 0;

    private StageSelectView stageSelectView;

    private void Start()
    {
        inp = ControllerManager.instance.CtrlInput;

        stageSelectView = GameObject.Find("Board").GetComponent<StageSelectView>();

        inp.Enable();

        stageSelectView.BrightUp(nowCursorPos);
    }

    private void Update()
    {
        // とりあえずキーボードで動くようにしてます。あとから変更します
        if (Input.GetKeyDown(KeyCode.Y))
        {
            // タイトルに戻る
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            //チュートリアルに進む
            SceneManager.LoadScene("TestTutorial");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            // メインゲームに進む
            SceneManager.sceneLoaded += GameSceneLoaded;

            SceneManager.LoadScene("Yokota");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && nowSelectStage > 1)
        {
            int key = -1;
            if (!CheckSpriteChange(key)) nowCursorPos -= 2;
            else stageSelectView.SpriteChange(key);
            nowSelectStage -= 2;
            DevLog.Log(nowSelectStage.ToString());
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && nowSelectStage < stageSelectView.StageNum - 2)
        {
            int key = 1;
            if (!CheckSpriteChange(key)) nowCursorPos += 2;
            else stageSelectView.SpriteChange(key);
            nowSelectStage += 2;
            DevLog.Log(nowSelectStage.ToString());
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && nowSelectStage % 2 != 0)
        {
            nowCursorPos--;
            nowSelectStage--;
            DevLog.Log(nowSelectStage.ToString());
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && nowSelectStage % 2 == 0)
        {
            nowCursorPos++;
            nowSelectStage++;
            DevLog.Log(nowSelectStage.ToString());
        }
        stageSelectView.BrightUp(nowCursorPos);
    }

    private bool CheckSpriteChange(int key)
    {
        if (key > 0)
        {
            if (nowSelectStage < 2 
                || nowSelectStage > stageSelectView.StageNum - 5) return false;
            else return true;
        }
        else
        {
            if (nowSelectStage < 5
                || nowSelectStage > stageSelectView.StageNum - 2) return false;
            else return true;
        }
    }

    private void GameSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var mapGenerater = GameObject.FindWithTag("MapGenerater").GetComponent<CSVMapGenerate>();

        mapGenerater.LoadStageNum = nowSelectStage + 1;

        SceneManager.sceneLoaded -= GameSceneLoaded;
    }
}
