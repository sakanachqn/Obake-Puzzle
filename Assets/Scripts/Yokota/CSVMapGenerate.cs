using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVMapGenerate : MonoBehaviour
{

    // マップ上すべてのブロックの文字情報
    private List<List<string>> allBlocksStr = new List<List<string>>();
    // 文字からオブジェクトを取り出せるディクショナリ
    private Dictionary<string, GameObject> nameToObject = new Dictionary<string, GameObject>();
    // マップ上すべてのブロックの実体
    private List<List<List<GameObject>>> allBlocksObj = new List<List<List<GameObject>>>();


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
    

    private void Start()
    {
        Init();
        ReadCsv();
        MapGenerate();
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
    }

    /// <summary>
    /// csvファイルを読み込んで、カンマ区切りで文字情報をListに格納
    /// </summary>
    public void ReadCsv()
    {   
        // 一時入力用の文字列
        string str;
        // ResourcesからCSVを読み込むのに必要
        TextAsset csvFile;
        // 読み込んだCSVファイルをカンマ区切りで格納するList
        List<string[]> csvDatas = new List<string[]>();

        // CSVファイルの行数
        int height = 0;
        
        // CSVファイルを読み込み
        csvFile = Resources.Load("CSV/Yokota/KikuchiTest") as TextAsset;
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
        // CSVファイルの行数分繰り返し
        for (int i = 0; i < height; i++)
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
    }

    /// <summary>
    /// マップ生成関数
    /// </summary>
    private void MapGenerate()
    {
        // マップの行数分繰り返し
        for (int x = 0; x < allBlocksStr.Count; x++)
        {
            // 一時入力用の二次元リスト
            List<List<GameObject>> zAxisObjList = new List<List<GameObject>>();

            // マップの列数分繰り返し
            for (int z = 0; z < allBlocksStr[x].Count; z++)
            {
                // x行z列にあるブロックたちの文字情報
                string tmpstr = allBlocksStr[x][z];
                // 一時入力用のリスト
                List<GameObject> yAxisObjList = new List<GameObject>();

                // x行z列にあるブロックの数だけ繰り返す
                for (int y = 0; y < tmpstr.Length; y++)
                {
                    // tmpstrの(y+1)文字目を取り出す
                    string objName = tmpstr.Substring(y, 1);
                    // 文字列からオブジェクトを取り出して生成
                    Instantiate(nameToObject[objName], new Vector3(x, y, z), Quaternion.identity);

                    // オブジェクトをリストに格納
                    yAxisObjList.Add(nameToObject[objName]);
                }

                // y軸にとったオブジェクトのリストをリストに格納
                zAxisObjList.Add(yAxisObjList);
            }

            // y-z平面のリストをリストに格納
            allBlocksObj.Add(zAxisObjList);
        }
    }
}
