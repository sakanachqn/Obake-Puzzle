using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSV_Dictionary : MonoBehaviour
{
    public List<string> Gimmicks = new List<string>();

    private Dictionary<string, GameObject> nameToObject = new Dictionary<string, GameObject>();

    private GameObject[,,] gimmicks = new GameObject[5, 4, 5];

    [SerializeField]
    private GameObject woodBox;
    [SerializeField]
    private GameObject steelBox;
    [SerializeField]
    private GameObject goal;
    [SerializeField]
    private GameObject pitFall;
    [SerializeField]
    private GameObject ground;

    private void Start()
    {
        Init();
        Gimmicks = ReadCsv();
    }

    void Init()
    {
        nameToObject.Add("木箱", woodBox);
        nameToObject.Add("鉄箱", steelBox);
        nameToObject.Add("星箱", goal);
        nameToObject.Add("落穴", pitFall);
        nameToObject.Add("地面", ground);
    }

    public List<string> ReadCsv()
    {   
        string str;
        List<string> dictionaryGimicks = new List<string>();

        TextAsset csvFile;
        List<string[]> csvDatas = new List<string[]>();

        int height = 0;
        
        csvFile = Resources.Load("CSV/Yokota/csvTest4") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
            height++;
        }
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < csvDatas[i].Length; j++)
            {
                str = csvDatas[i][j];
                dictionaryGimicks.Add(str);
            }
        }

        Debug.Log(dictionaryGimicks.Count);

        for (int x = 0; x < dictionaryGimicks.Count; x++)
        {
            string tmpstr = dictionaryGimicks[x];
            int length = 0;
            if (tmpstr.Length == 2)
            {
                length = tmpstr.Length;
            }
            else
            {
                length = tmpstr.Length - 1;
            }
            for (int y = 0; y < length; y++)
            {
                if (y % 2 == 0)
                {
                    string objName = tmpstr.Substring(y, 2);
                    if (x < 5)
                    {
                        gimmicks[x, y / 2, 0] = Instantiate(nameToObject[objName], new Vector3(x, y / 2, 0), Quaternion.identity);
                    }
                    else if (x < 10)
                    {
                        gimmicks[x - 5, y / 2, 0]  = Instantiate(nameToObject[objName], new Vector3(x - 5, y / 2, 1), Quaternion.identity);
                    }
                    else if (x < 15)
                    {
                        gimmicks[x - 10, y / 2, 0] = Instantiate(nameToObject[objName], new Vector3(x - 10, y / 2, 2), Quaternion.identity);
                    }
                    else if (x < 20)
                    {
                        gimmicks[x -15, y / 2, 0] = Instantiate(nameToObject[objName], new Vector3(x - 15, y / 2, 3), Quaternion.identity);
                    }
                    else if (x < 25)
                    {
                        gimmicks[x -20, y / 2, 0] = Instantiate(nameToObject[objName], new Vector3(x - 20, y / 2, 4), Quaternion.identity);
                    }
                }
            }
        }

        return dictionaryGimicks;
    }
}
