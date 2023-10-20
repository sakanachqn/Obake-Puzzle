using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectCheck : MonoBehaviour
{
    public bool CheckObject(Vector3 targetDirec)
    {
        Debug.DrawRay(this.transform.position,targetDirec, Color.cyan, Mathf.Infinity);
        if (Physics.Raycast(this.transform.position, targetDirec, out var hitObj, 3))
        {
            Debug.Log(hitObj.collider.name);
            return true;
        }
        else  return false;
    }
}
