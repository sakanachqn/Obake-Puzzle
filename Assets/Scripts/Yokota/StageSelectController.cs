using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StageSelectController : MonoBehaviour
{
    private ControllerInput inp;

    // 今どのステージを選んでいるか
    private int nowSelectStage = 0;

    // 今どのイメージににカーソルがあっているか
    private int nowCursorPos = 0;

    [SerializeField]
    private StageSelectView stageSelectView;

    private SceneFade sceneFade;

    private Vector2 stickInclination;

    private void Start()
    {
        inp = ControllerManager.instance.CtrlInput;

        stageSelectView = GameObject.Find("Board").GetComponent<StageSelectView>();

        inp.Enable();
    }

    private async void Update()
    {
        // とりあえずキーボードで動くようにしてます。あとから変更します
        if (inp.StageSelect.Back.WasPressedThisFrame())
        {
            // タイトルに戻る
            await sceneFade.SceneChange("TitleScene");
        }
        
        if (inp.StageSelect.Tutorial.WasPressedThisFrame())
        {
            //チュートリアルに進む
            //await sceneFade.SceneChange("")
        }

        if (inp.StageSelect.Decision.WasPressedThisFrame())
        {
            // メインゲームに進む
            SceneManager.sceneLoaded += GameSceneLoaded;

            await sceneFade.SceneChange("GameScene");
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
            if (nowSelectStage > stageSelectView.StageNum - 3
                || nowSelectStage < stageSelectView.StageNum - 4) return false;
            else return true;
        }
    }

    private void GameSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var mapGenerater = GameObject.FindWithTag("MapGenerater").GetComponent<CSVMapGenerate>();

        mapGenerater.LoadStageNum = nowSelectStage + 1;

        SceneManager.sceneLoaded -= GameSceneLoaded;
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        // performedコールバックだけをチェックする
        if (!context.performed) return;

        // スティックの2軸入力取得
        stickInclination = context.ReadValue<Vector2>();

        if (stickInclination.x == -1 && nowSelectStage > 1)
        {
            int key = -1;
            if (CheckSpriteChange(key)) stageSelectView.SpriteChange(key);
            else nowCursorPos -= 2;
            nowSelectStage -= 2;
        }
        if (stickInclination.x == 1 && nowSelectStage < stageSelectView.StageNum - 2)
        {
            int key = 1;
            if (CheckSpriteChange(key)) stageSelectView.SpriteChange(key);
            else nowCursorPos += 2;
            nowSelectStage += 2;
        }
        if (stickInclination.y == 1 && nowSelectStage % 2 != 0)
        {
            nowCursorPos--;
            nowSelectStage--;
        }
        if (stickInclination.y == -1 && nowSelectStage % 2 == 0)
        {
            nowCursorPos++;
            nowSelectStage++;
        }
    }
}
