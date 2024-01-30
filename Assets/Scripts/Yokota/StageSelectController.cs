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

    public static int SelectedStage;

    private StageSelectView stageSelectView;

    private SceneFade sceneFade;

    // スティックの傾きを保存する変数
    private Vector2 stickInclination;

    private void Start()
    {
        inp = ControllerManager.instance.CtrlInput;

        stageSelectView = GetComponent<StageSelectView>();

        // 動作試験の際に適当に使ったものです。モック版のときはタイトルシーンから使っている
        // イメージからシーンフェードのスクリプトをとってきてください
        sceneFade = SceneFade.instance;

        inp.Enable();
    }

    private async void Update()
    {
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
            SoundManager.Instance.Play("Select");
            SelectedStage = nowSelectStage + 1;
            await sceneFade.SceneChange("GameScene");
        }

        stageSelectView.BrightUp(nowCursorPos);
    }

    /// <summary>
    /// スティックが倒されたときに読まれる関数
    /// </summary>
    /// <param name="context"></param>
    public void OnNavigate(InputAction.CallbackContext context)
    {
        // performedコールバックだけをチェックする
        if (!context.performed) return;

        // スティックの2軸入力取得
        stickInclination = context.ReadValue<Vector2>();

        // スティックが左に傾いていて、カーソルが左端にないとき
        if (stickInclination.x == -1 && nowSelectStage > 0)
        {
            // key:左が入力されたときに-1、右が入力されたときに1を設定する
            int key = -1;
            // イメージのスプライトを変える必要があれば変更する
            if (CheckSpriteChange(key)) stageSelectView.SpriteChange(key);
            // 必要がなければカーソルの位置を左へ動かす
            else nowCursorPos -= 1;
            // 今選ぼうとしているステージ番号を更新する
            nowSelectStage -= 1;
        }
        // スティックが右に傾いていて、カーソルが右端にないとき
        if (stickInclination.x == 1 && nowSelectStage < stageSelectView.StageNum - 1)
        {
            int key = 1;
            // イメージのスプライトを変える必要があれば変更する
            if (CheckSpriteChange(key)) stageSelectView.SpriteChange(key);
            // 必要がなければカーソルの位置を右へ動かす
            else nowCursorPos += 1;
            // 今選ぼうとしているステージ番号を更新する
            nowSelectStage += 1;
        }
        // スティックが上に傾いていて、カーソルが上側にないとき
        //if (stickInclination.y == 1 && nowSelectStage % 2 != 0)
        //{
        //    // カーソルの位置を上へ動かす
        //    nowCursorPos--;
        //    // 今選ぼうとしているステージ番号を更新する
        //    nowSelectStage--;
        //}
        //// スティックが下に傾いていて、カーソルが上側にあるとき
        //if (stickInclination.y == -1 && nowSelectStage % 2 == 0)
        //{
        //    // カーソルの位置を上へ動かす
        //    nowCursorPos++;
        //    // 今選ぼうとしているステージ番号を更新する
        //    nowSelectStage++;
        //}
    }

    /// <summary>
    /// スティック操作があったときにイメージのスプライトを変える必要があるか判定する関数
    /// </summary>
    /// <param name="key">入力が右、左のどちらか判定する変数。右: 1、左: -1 </param>
    /// <returns></returns>
    private bool CheckSpriteChange(int key)
    {
        if (key > 0)
        {
            // カーソルが左端、または右端から2番目より右にある時
            if (nowSelectStage < 2
                || nowSelectStage > stageSelectView.StageNum - 5) return false; // 変更しない
            else return true;   // 変更する
        }
        else
        {
            // カーソルが右端、または左端から2番目より左にある時
            if (nowSelectStage > stageSelectView.StageNum - 3
                || nowSelectStage < stageSelectView.StageNum - 4) return false; // 変更しない
            else return true;   // 変更する
        }
    }

    /// <summary>
    /// ゲームシーンへ遷移するときに今何番目のステージを選択したか知らせる関数
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    //private void GameSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    // マップを生成するスクリプトを探す
    //    var mapGenerater = GameObject.FindWithTag("MapGenerater").GetComponent<CSVMapGenerate>();

    //    // 読み込むステージの番号を書き換える
    //    mapGenerater.LoadStageNum = nowSelectStage + 1;

    //    // この処理を削除する
    //    SceneManager.sceneLoaded -= GameSceneLoaded;
    //}
}
