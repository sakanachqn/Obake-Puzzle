using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;//DOTween

public class GoalObjController : MonoBehaviour
{
    [SerializeField]
    private float groundY = 0f;//地面のY座標

    private bool isGoal = false;//ゴールフラグ

    /// <summary>
    /// ゴール判定
    /// </summary>
    /// <param name="collision">ぶつかったオブジェクト</param>
    private void OnCollisionEnter(Collision collision)
    {
        //y座標が地面座標以外(地面にくっついていない)なら、ゴール判定を無効化
        if (this.gameObject.transform.position.y != groundY) return;

        //タグがPlayer(オバケ)なら
        if (collision.gameObject.tag == "Player")
        {
            //ゴールする
            Debug.Log("ゴール");
            isGoal = true;
        }
    }
}
