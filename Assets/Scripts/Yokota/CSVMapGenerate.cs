using Palmmedia.ReportGenerator.Core.Common;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CSVMapGenerate : MonoBehaviour
{
    private static CSVMapGenerate instance;
    public static CSVMapGenerate Instance => instance;
    // マップ上すべてのブロックの文字情報
    private List<List<string>> allBlocksStr = new List<List<string>>();
    // 文字からオブジェクトを取り出せるディクショナリ
    private Dictionary<string, GameObject> nameToObject = new Dictionary<string, GameObject>();
    // マップ上すべてのブロックの実体
    private List<List<List<GameObject>>> allBlocksObj = new List<List<List<GameObject>>>();

    // 一時入力用の二次元リスト
    List<List<GameObject>> zAxisObjList = new List<List<GameObject>>();
    // 一時入力用のリスト
    List<GameObject> yAxisObjList = new List<GameObject>();

    // 呼び出すマップCSVファイルの番号
    public int LoadStageNum;

    // マップ生成完了フラグ
    public static bool IsMapGenerate = false;

    // ResourcesからCSVを読み込むのに必要
    private TextAsset csvFile;

    // 読み込んだCSVファイルをカンマ区切りで格納するList
    private List<string[]> csvDatas = new List<string[]>();

    // 選ばれたステージナンバー
    private int selectedStage;

    /*
    SkillName, SkillCastLimitについて
    どちらの配列も 0番目の要素が Xボタン、1番目の要素が Yボタンに対応しています
    SkillCastLimit はスキルを使用できる数が入っています。
    SkillName はスキル名の文字が入っています。
    入っている文字は F(Fire), W(Water), S(Suction)の三種類が入っています。
    */

    // スキル名が入った文字列リスト
    public List<string> SkillName;

    // スキルの使用可能回数の整数リスト
    public List<int> SkillCastLimit;

    // CSVファイルの何行目からスキルの情報が書いてあるか
    private int skillReadStartPoint = 7;

    /*
    allBlocksObjについて
    allBlocksObj[x座標][z座標][y座標]でそのオブジェクトの情報をとることができます。
    */

    [SerializeField, Header("地面")]
    private GameObject ground;
    [SerializeField,Header("木箱")]
    private GameObject woodBox;
    [SerializeField,Header("鉄箱")]
    private GameObject steelBox;
    [SerializeField,Header("落とし穴")]
    private GameObject pitFall;
    [SerializeField, Header("ゴール（地面）")]
    private GameObject goal;
    [SerializeField,Header("ゴール（鉄箱）")]
    private GameObject steelGoal;
    [SerializeField,Header("プレイヤー")]
    private GameObject player;

    [SerializeField, Header("地面マテリアル（白, 黒）")]
    private Material[] groundMaterial = new Material[2];
    [SerializeField, Header("落とし穴マテリアル（白, 黒）")]
    private Material[] pitFallMaterial = new Material[2];

    private void Awake()
    {
        Init();
        ReadCsv();
        MapGenerate();

        if(instance == null) instance = this;
    }

    /// <summary>
    /// Dictionaryにオブジェクトとその名前を登録
    /// </summary>
    private void Init()
    {
        nameToObject.Add("1", ground);
        nameToObject.Add("2", woodBox);
        nameToObject.Add("3", steelBox);
        nameToObject.Add("4", pitFall);
        nameToObject.Add("5", goal);
        nameToObject.Add("6", steelGoal);
        nameToObject.Add("7", player);

        selectedStage = StageSelectController.SelectedStage;
    }

    /// <summary>
    /// csvファイルを読み込んで、カンマ区切りで文字情報をListに格納
    /// </summary>
    public void ReadCsv()
    {   
        // 一時入力用の文字列
        string str;

        // CSVファイルの行数
        int height = 0;
        
        // CSVファイルを読み込み
        //if (selectedStage < 10)
        //    csvFile = Resources.Load("CSV/MapData/TrialMap_0" + selectedStage) as TextAsset;
        //else
        //    csvFile = Resources.Load("CSV/MapData/TrialMap_" + selectedStage) as TextAsset;

        // スキル読み込み用に読み込むCSVファイル変えています   横田
        if (selectedStage < 10)
            csvFile = Resources.Load("CSV/MapData/SkillReadTest_0" + selectedStage) as TextAsset;
        else
            csvFile = Resources.Load("CSV/MapData/SkillReadTest" + selectedStage) as TextAsset;
        // 読み込んだテキストをString型にして格納
        StringReader reader = new StringReader(csvFile.text);

        // 読み込んだテキストの終わりまで繰り返し
        while (reader.Peek() > -1)
        {
            // 一行分の文字列を格納
            string line = reader.ReadLine();
            // ,で区切ってListに格納
            csvDatas.Add(line.Split(','));
            // 行数加算
            height++;
        }

        // マップの部分だけ Listに格納
        for (int i = 0; i < 5; i++)
        {
            // 一時入力用のリスト
            List<string> blockStrList = new List<string>();

            // i行目の列数文繰り返し
            for (int j = 0; j < csvDatas[i].Length; j++)
            {
                // iは行数、jは列数
                str = csvDatas[i][j];
                // i行j列にあるすべてのオブジェクトの文字情報を格納
                blockStrList.Add(str);
            }

            // 一行分のブロックのリストを格納
            allBlocksStr.Add(blockStrList);
        }
        
        // スキル名の読み込み
        for (int i = 0; i < csvDatas[skillReadStartPoint].Length; i++)
        {
            str = csvDatas[skillReadStartPoint][i];
            // 文字の入っていないセルを飛ばす
            if (str == "") continue;
            SkillName.Add(str);
        }

        skillReadStartPoint++;

        // スキルの発動回数の読み込み
        for (int i = 0; i < csvDatas[skillReadStartPoint].Length; i++)
        {
            str = csvDatas[skillReadStartPoint][i];
            // 文字の入っていないセルを飛ばす
            if (str == "") continue;
            SkillCastLimit.Add(str.ParseLargeInteger());
        }
    }

    /// <summary>
    /// マップ生成関数
    /// </summary>
    private void MapGenerate()
    {
        // マップの行数分繰り返し
        for (int x = 0; x < allBlocksStr.Count; x++)
        {
            ZAxisGenerate(x);
        }

        // マップ生成完了フラグを立てる
        IsMapGenerate = true;
    }

    private void ZAxisGenerate(int x)
    {
        // マップの列数分繰り返し
        for (int z = 0; z < allBlocksStr[x].Count; z++)
        {
            YAxisGenerate(x, z);
        }

        // y-z平面のリストをリストに格納
        allBlocksObj.Add(zAxisObjList);
    }

    private void YAxisGenerate(int x, int z)
    {
        // x行z列にあるブロックたちの文字情報
        string tmpstr = allBlocksStr[x][z];

        // x行z列にあるブロックの数だけ繰り返す
        for (int y = 0; y < tmpstr.Length; y++)
        {
            // tmpstrの(y+1)文字目を取り出す
            string objName = tmpstr.Substring(y, 1);

            CreateCheckPattarn(x, y, z, objName);

            // プレイヤーをさすオブジェクトのときはスルーする
            if (objName == "7") break;
            // オブジェクトをリストに格納
            else yAxisObjList.Add(nameToObject[objName]);

        }

        // y軸にとったオブジェクトのリストをリストに格納
        zAxisObjList.Add(yAxisObjList);
    }

    /// <summary>
    /// マップの地面を市松模様化する関数
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="objName"></param>
    private void CreateCheckPattarn(int x, int y, int z, string objName)
    {
        if (y == 0)
        {
            GameObject tmpObj;

            int pattarn = (x + z) % 2;

            if (pattarn == 0)
            {
                if (objName == "1")
                {
                    // 文字列からオブジェクトを取り出して生成
                    tmpObj = Instantiate(nameToObject[objName], new Vector3(x, y, z), Quaternion.identity);
                    tmpObj.GetComponent<Renderer>().material = groundMaterial[0];
                }
                else
                {
                    // 文字列からオブジェクトを取り出して生成
                    tmpObj = Instantiate(nameToObject[objName], new Vector3(x, y, z), Quaternion.identity);
                    tmpObj.GetComponent<Renderer>().material = pitFallMaterial[0];
                }
            }
            if (pattarn != 0)
            {
                if (objName == "1")
                {
                    // 文字列からオブジェクトを取り出して生成
                    tmpObj = Instantiate(nameToObject[objName], new Vector3(x, y, z), Quaternion.identity);
                    tmpObj.GetComponent<Renderer>().material = groundMaterial[1];
                }
                else
                {
                    // 文字列からオブジェクトを取り出して生成
                    tmpObj = Instantiate(nameToObject[objName], new Vector3(x, y, z), Quaternion.identity);
                    tmpObj.GetComponent<Renderer>().material = pitFallMaterial[1];
                }
            }
        }
        else
        {
            Instantiate(nameToObject[objName], new Vector3(x, y, z), Quaternion.identity);
        }
    }

    /// <summary>
    /// ゲームシーンを再読み込みする関数
    /// </summary>
    public async void Regenerate()
    {
        SoundManager.Instance.Play("Select");
        await SceneFade.instance.SceneChange("GameScene");
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
    //    mapGenerater.LoadStageNum = LoadStageNum;

    //    // この処理を削除する
    //    SceneManager.sceneLoaded -= GameSceneLoaded;
    //}
}
