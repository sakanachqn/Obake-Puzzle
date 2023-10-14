using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSV_Dictionary : MonoBehaviour
{
    private List<List<string>> allGimmicks = new List<List<string>>();

    private Dictionary<string, GameObject> nameToObject = new Dictionary<string, GameObject>();

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
        ReadCsv();
    }

    void Init()
    {
        nameToObject.Add("木箱", woodBox);
        nameToObject.Add("鉄箱", steelBox);
        nameToObject.Add("星箱", goal);
        nameToObject.Add("落穴", pitFall);
        nameToObject.Add("地面", ground);
    }

    public void ReadCsv()
    {   
        string str;

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
            List<string> dictionaryGimicks = new List<string>();

            for (int j = 0; j < csvDatas[i].Length; j++)
            {
                str = csvDatas[i][j];
                dictionaryGimicks.Add(str);
            }

            allGimmicks.Add(dictionaryGimicks);
        }

        for (int x = 0; x < allGimmicks.Count; x++)
        {
            for (int z = 0; z < allGimmicks[x].Count; z++)
            {
                string tmpstr = allGimmicks[x][z];
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
                        Instantiate(nameToObject[objName], new Vector3(x, y / 2, z), Quaternion.identity);
                    }
                }
            }
        }
    }
}
