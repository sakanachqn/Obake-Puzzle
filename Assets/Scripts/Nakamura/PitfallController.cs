using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;//DOTween

public class PitfallController : MonoBehaviour
{
    [SerializeField]
    private float destroyWoodenBoxTime = 0.5f;//木箱を消滅させるまでの時間
    [SerializeField]
    private float moveIronBoxTime = 1f;//鉄箱を移動させる時間

    private void OnCollisionEnter(Collision collision)
    {
        //タグがWoodenBox(木箱)なら
        if (collision.gameObject.tag == "WoodenBox")
        {
            //木箱を消滅させる
            DestroyWoodenBox(collision);
        }

        //タグがIronBox(鉄箱)なら
        if (collision.gameObject.tag == "IronBox")
        {
            //鉄箱を移動させる
            MoveIronBox(collision);
        }
    }

    /// <summary>
    /// 鉄箱が来たら落とし穴を埋めて地面になる
    /// </summary>
    /// <param name="collision">ぶつかったオブジェクト</param>
    private void MoveIronBox(Collision collision)
    {
        //moveIronBoxTime秒かけて落とし穴の位置へ移動する
        Vector3 pos = this.gameObject.transform.position;
        collision.transform.DOMove(new Vector3(pos.x, pos.y, pos.z), moveIronBoxTime);

        //自身(落とし穴)を消滅させる
        Destroy(this.gameObject);
    }

    /// <summary>
    /// 木箱が来たら落ちて消滅させる
    /// </summary>
    /// <param name="collision">ぶつかったオブジェクト</param>
    private void DestroyWoodenBox(Collision collision)
    {
        //対象の重力を有効化する
        Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody>();
        rigidbody.useGravity = true;

        //自身のisTrigger有効化する
        BoxCollider boxCollider = this.gameObject.GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;

        //destroyWoodenBoxTime後に対象を消滅させる
        DOVirtual.DelayedCall(destroyWoodenBoxTime, () => Destroy(collision.gameObject));

        //自身のisTrigger無効化する
        boxCollider.isTrigger = false;
    }
}
