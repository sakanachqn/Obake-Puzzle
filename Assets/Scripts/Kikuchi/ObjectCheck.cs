using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectCheck : MonoBehaviour
{
    public void CheckObject(Vector3 targetDirec)
    {
        Debug.Log("Shot");
        Debug.DrawRay(this.transform.position, targetDirec, Color.cyan, Mathf.Infinity);
        if (Physics.Raycast(this.transform.position, targetDirec, out var hitObj, 1))
        {
            Debug.Log("hit");
            Debug.Log(hitObj);
        }
    }
}
