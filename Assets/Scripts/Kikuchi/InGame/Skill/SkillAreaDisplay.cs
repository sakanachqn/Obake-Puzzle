using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAreaDisplay : MonoBehaviour
{
    private SkillManager smg;

    // スキル発動時の範囲を示すTransform
    [SerializeField] private Transform fireArea;

    // スキル範囲のTransformリスト
    private List<Transform> fireSkillArea = new List<Transform>();

    // スキル範囲のオブジェクトとそのマテリアルの辞書
    private Dictionary<GameObject, Material> hitArea = new Dictionary<GameObject, Material>();

    // スキル範囲内のオブジェクト情報用リスト
    private List<GameObject> hitObj = new List<GameObject>();

    // スキル範囲のフェード用の新しいマテリアル
    [SerializeField] private Material changeMat;

    // スキル範囲のフェード時間
    [SerializeField] private float fadeTime = 1;

    // スキル範囲の下部の透明度
    [SerializeField] private float underAlpha = 0.3f;

    // スキル範囲のプレハブ
    [SerializeField] private GameObject skillAreaPrefab;

    // スキル発動位置のプレハブ
    [SerializeField] private GameObject skillPosPrefab;

    // デフォルトのマテリアル
    private Material mat;

    [HideInInspector]
    // スキル範囲が表示中かどうか
    public bool areaViewNow = false;

    public GameObject posObj = null;
    private GameObject parent = null;

    // Start is called before the first frame update
    void Start()
    {
        smg = GetComponent<SkillManager>();
        // スキル範囲のTransformをリストに追加
        var index = fireArea.childCount;
        for (int i = 0; i < index; i++)
        {
            fireSkillArea.Add(fireArea.GetChild(i));
        }
    }

    /// <summary>
    /// スキル範囲の表示を行うメソッド
    /// </summary>
    public void ShowSkillArea(bool isFire = false)
    {
        // スキル範囲をまとめる親のオブジェクトを作成
        parent = new GameObject("Area");

        if (isFire)
        {
            foreach (Transform t in fireSkillArea)
            {
                // スキル範囲の下にRayを飛ばして、地面にヒットしたらスキル範囲を表示
                if (Physics.Raycast(t.position, Vector3.down, out var hit, 100))
                {
                    // "Pitfall"タグのオブジェクトにヒットした場合はスキップ
                    if (hit.collider.tag == "Pitfall")
                        return;

                    // オブジェクトにヒットした場合、スキル範囲を生成
                    if (hit.transform.position.y != 0)
                    {
                        hitObj.Add(hit.collider.gameObject);
                        hit.collider.gameObject.SetActive(false);
                        Vector3 pos = hit.transform.position;
                        createArea(pos);
                    }

                    // 地面にヒットした場合、上にずらして生成
                    if (hit.transform.position.y == 0)
                    {
                        Vector3 pos = hit.transform.position;
                        pos.y += 1;
                        createArea(pos);
                    }
                }
            }
        }
        else
        {
            Vector3 currentPos = this.transform.position;

            Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };

            foreach(Vector3 direction in directions)
            {
                if (Physics.Raycast(currentPos, direction, out var hit, 1))
                {
                    hitObj.Add(hit.collider.gameObject);
                    hit.collider.gameObject.SetActive(false);
                    createArea(direction + this.transform.position);
                }
                else
                {
                    createArea(direction + this.transform.position);
                }
            }
        }

        areaViewNow = true;
        // スキル発動位置のオブジェクトを生成
        if (posObj == null)
        {
            posObj = Instantiate(skillPosPrefab, this.transform.position + Vector3.right , Quaternion.identity);
        }
    }

    private void createArea(Vector3 pos)
    {
        var temp = Instantiate(skillAreaPrefab, pos, Quaternion.identity, parent.transform);
        var tempMat = temp.GetComponent<MeshRenderer>();
        hitArea.Add(temp, mat);
        tempMat.material = changeMat;
        tempMat.material.DOFade(underAlpha, fadeTime).SetLoops(-1, LoopType.Yoyo);
    }

    /// <summary>
    /// スキル範囲を破棄するメソッド
    /// </summary>
    public void HideSkillArea()
    {
        // スキル範囲のオブジェクトと元のマテリアルに戻す
        foreach (KeyValuePair<GameObject, Material> kvp in hitArea)
        {
            kvp.Key.GetComponent<MeshRenderer>().material = kvp.Value;

            // タグが"clone"の場合はオブジェクトを破棄
            if (kvp.Key.tag == "clone")
                Destroy(kvp.Key.gameObject);
        }

        foreach (var obj in hitObj)
        {
            obj.SetActive(true);
        }

        // 辞書をクリアし、スキル発動中のフラグをオフに
        hitObj.Clear();
        hitArea.Clear();
        Destroy(parent.gameObject);
        parent = null;
        SkillManager.IsNowSkill = false;
        areaViewNow = false;
        if (posObj != null)
        {
            Destroy(posObj);
            posObj = null;
        }
        smg.ctrl.CtrlInput.Player.Move.Enable();
    }

    

    ///// <summary>
    ///// 移動先にobjectがあるか確認するメソッド
    ///// </summary>
    ///// <param name="targetDirec">スティックの方角</param>
    ///// <returns>移動先にobjがあったらtrue</returns>
    //public bool CheckObject(Vector3 startPos, Vector3 targetDirec, out string name)
    //{
    //    //移動したい方向にrayを飛ばしてオブジェクトがあるか確認
    //    if (Physics.Raycast(startPos, targetDirec, out var hitObj, 1))
    //    {
    //        if (hitObj.collider.tag == "Pitfall")
    //        {
    //            name = null;
    //            return false;
    //        }
    //        else if (hitObj.collider.tag == "clone")
    //        {
    //            name = null;
    //            return true;
    //        }
    //        else if (hitObj.collider.tag == "Player")
    //        {
    //            name = hitObj.collider.tag;
    //            return true;
    //        }
    //        else
    //        {
    //            name = null;
    //            return false;
    //        }
    //    }
    //    else
    //    {
    //        name = null;
    //        return false;
    //    }
    //}

    ///// <summary>
    ///// マップ外かどうかのチェック
    ///// </summary>
    ///// <param name="targetDirec">スティックの方角</param>
    ///// <returns>オブジェクトがなかったらfalse</returns>
    //public bool CheckOffMap(Vector3 targetDirec)
    //{
    //    Vector3 startPos = posObj.transform.position + targetDirec;
    //    //移動先から真下にray飛ばして地面があるか確認
    //    if (CheckObject(startPos, Vector3.down, out var name))
    //    {
    //        return true;
    //    }
    //    else return false; ;
    //}
}
