using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObakeAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator _obake;

    private static ObakeAnimation _instance;
    public static ObakeAnimation Inctance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ObakeAnimation>();

                if (_instance == null)
                {
                    var obj = new GameObject("ObakeAnimation");
                    _instance = obj.AddComponent<ObakeAnimation>();
                }
            }
            return _instance;
        }
    }

    void Start()
    {
        // Animatorを自身がついているオブジェクトから取得するように変更(菊池)
        _obake = this.gameObject.transform.root.GetComponent<Animator>();

    }

    /// <summary>
    /// 待機アニメーション
    /// </summary>
    public void IdleAnimation()
    {
        _obake.SetBool("Bool", true);
    }

    /// <summary>
    /// 歩くアニメーション
    /// </summary>
    public void WalkAnimation(bool tag = false)
    {
        Debug.Log("walkAnim");
        _obake.SetBool("Walk", tag);
    }

    /// <summary>
    /// 炎スキルアニメーション
    /// </summary>
    public void FlameAnimation()
    {
        _obake.SetTrigger("Flame");
    }

    /// <summary>
    /// 炎スキルのミスアニメーション
    /// </summary>
    public void FlameMissAnimation()
    {
        _obake.SetTrigger("FlameMiss");
    }

    /// <summary>
    /// 水スキルアニメーション
    /// </summary>
    public void WaterAnimation()
    {
        _obake.SetTrigger("Water");
    }

    /// <summary>
    /// 水スキルのミスアニメーション
    /// </summary>
    public void WaterMissAnimation()
    {
        _obake.SetTrigger("WaterMiss");
    }

    /// <summary>
    /// 吸い込みアニメーション
    /// </summary>
    public void SuctionAnimation()
    {
        _obake.SetTrigger("Suction");
        _obake.SetBool("SuctionWhile", true);
    }


    /// <summary>
    /// 吸い込みのミスアニメーション
    /// </summary>
    public void SuctionMissAnimation()
    {
        _obake.SetTrigger("SuctionMiss");
    }

    /// <summary>
    /// 吐き出しアニメーション
    /// </summary>
    public void SpittingoutAnimation()
    {
        _obake.SetBool("SuctionWhile", false);
        _obake.SetTrigger("Spittingout");
    }
}
