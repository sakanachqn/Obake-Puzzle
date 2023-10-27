using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private void Start()
    {
        CheckSelectArea(2);
    }

    private void CheckSelectArea(int range)
    {
        Debug.DrawRay(this.transform.position , new Vector3(range, 0, 0), Color.cyan, Mathf.Infinity);
    }
}
