using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReadCSV : MonoBehaviour
{
    public List<string> strings = new List<string>();

    public List<int> ints = new List<int>();

    private void Start()
    {
        ints = ReadCsv_int();
        strings = ReadCsv_string();
    }

    public List<string> ReadCsv_string()
    {
        string _string;
        List<string> listString = new List<string>();

        TextAsset csvFile;

        List<string[]> csvDatas = new List<string[]>();

        int height = 0;

        int i = 0;

        csvFile = Resources.Load("CSV/Yokota/csvTest3") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
            height++;
        }
        for (i = 0; i < height; i++)
        {
            _string = csvDatas[i][0];
            listString.Add(_string);
        }
        return listString;
    }

    public List<int> ReadCsv_int()
    {
        int tmpNum;
        List<int> list = new List<int>();

        TextAsset csvFile;

        List<string[]> csvDatas = new List<string[]>();

        int height = 0;

        int i = 0;

        csvFile = Resources.Load("CSV/Yokota/csvTest3") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
            height++;
        }
        for (i = 0; i < height; i++)
        {
            tmpNum = Convert.ToInt32(csvDatas[i][4]);
            list.Add(tmpNum);
        }

        return list;
    }
}
