using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepCounter : MonoBehaviour
{
    public int stepCountNum = 0;

    private static StepCounter _instance;
    public static StepCounter Inctance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<StepCounter>();

                if (_instance == null)
                {
                    var obj = new GameObject("StepCounter");
                    _instance = obj.AddComponent<StepCounter>();
                }
            }
            return _instance;
        }
    }

    public void StepCount()
    {
        stepCountNum++;
    }
}
