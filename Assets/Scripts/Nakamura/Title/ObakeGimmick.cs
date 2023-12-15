using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObakeGimmick : MonoBehaviour
{

    void Update()
    {
        //画面外に出たら消える
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(this.gameObject);
            TitleGimmickManager.obakeCount--;
        }
    }
}
