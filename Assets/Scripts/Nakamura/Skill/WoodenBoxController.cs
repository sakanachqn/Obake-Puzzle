using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;//DOTween

public class WoodenBoxController : MonoBehaviour
{
    [SerializeField]
    private float moveTime = 1f;//移動時間

    private static WoodenBoxController _instance;
    public static WoodenBoxController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance
                    = GameObject.FindObjectOfType<WoodenBoxController>();
            }
            return _instance;
        }
    }

    /// <summary>
    /// 炎スキルを使用されると消滅する
    /// </summary>
    public void DestroyFire()
    {
        //消滅
        Destroy(this.gameObject);
    }

    /// <summary>
    /// 水スキルを使用されると奥に一マス移動する
    /// </summary>
    /// <param name="_playerPos">プレイヤーの位置</param>
    public void MoveWater(Vector3 _playerPos)
    {
        //moveTime分の時間をかけて移動する
        this.gameObject.transform.DOMove(-_playerPos, moveTime);
    }
}
