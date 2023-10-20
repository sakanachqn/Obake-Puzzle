using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;//DOTween

public class IronBoxController : MonoBehaviour
{
    [SerializeField]
    private float inhaleTime = 0.5f;//吸い込み時間
    [SerializeField]
    private float spitOutTime = 0.5f;//吐き出し時間

    private static IronBoxController _instance;
    public static IronBoxController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance
                    = GameObject.FindObjectOfType<IronBoxController>();
            }
            return _instance;
        }
    }


    /// <summary>
    /// 吸い込んで保持
    /// </summary>
    /// <param name="_playerPos">プレイヤーの位置</param>
    /// <param name="_playerObj">プレイヤーオブジェクト</param>
    public void InhaleBox(Vector3 _playerPos,GameObject _playerObj)
    {
        //吸い込んで保持(移動)する
        this.gameObject.transform.DOMove(_playerPos, inhaleTime);

        //鉄箱とオバケの親子関係を作成
        this.gameObject.transform.parent = _playerObj.gameObject.transform;

        //非表示
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// 吐き出して配置
    /// </summary>
    /// <param name="_playerPos">プレイヤーの位置</param>
    public void SpitOutBox(Vector3 _playerPos)
    {
        //鉄箱とオバケの親子関係を解除
        this.gameObject.transform.parent = null;

        //表示
        this.gameObject.SetActive(true);

        //吐き出して配置(移動)する
        this.gameObject.transform.DOMove(-_playerPos, spitOutTime);
    }
}
