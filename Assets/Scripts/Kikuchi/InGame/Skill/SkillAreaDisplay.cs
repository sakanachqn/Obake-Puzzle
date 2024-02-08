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
    
    // スキル発動時の範囲を示すTransform
    [SerializeField] private Transform miniArea;

    // スキル範囲のTransformリスト
    private List<Transform> fireSkillArea = new List<Transform>();

    // スキル範囲のTransformリスト
    private List<Transform> miniSkillArea = new List<Transform>();

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

    private int cloneLayer = ~7;

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

        index = miniArea.childCount;
        for(int i = 0; i < index; i++)
        {
            miniSkillArea.Add(miniArea.GetChild(i));
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
                        //hit.collider.gameObject.SetActive(false);
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
            foreach (Transform t in miniSkillArea)
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
                        //hit.collider.gameObject.SetActive(false);
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

        areaViewNow = true;
        //// スキル発動位置のオブジェクトを生成
        //if (posObj == null)
        //{
        //    posObj = Instantiate(skillPosPrefab, this.transform.position + Vector3.right , Quaternion.identity);
        //}
    }

    private void createArea(Vector3 pos)
    {
        var temp = Instantiate(skillAreaPrefab, pos, Quaternion.identity, parent.transform);
        var tempMat = temp.GetComponent<MeshRenderer>();
        hitArea.Add(temp, mat);
        tempMat.material = changeMat;
        temp.GetComponent<AreaColorFade>().ColorFade();
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

        //foreach (var obj in hitObj)
        //{
        //    obj.SetActive(true);
        //}

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


    /// <summary>
    /// スキル発動位置を取得する非同期メソッド
    /// </summary>
    /// <returns></returns>
    public async void GetSkillPos()
    {
        smg.isPosSelectNow = true;

        var direcDic = smg.plCon.plMove.Directions;

        Vector3 rayPos;

        Vector3 firstRayPos = new Vector3(this.transform.position.x + direcDic[ControllerManager.instance.dPadDirection].x,
                                   5,
                                   this.transform.position.z + direcDic[ControllerManager.instance.dPadDirection].z);

        var testvar = new Vector3(0, -10, 0);

        if(posObj == null)
        {
            rayPos = firstRayPos;

        }
        else
        {
            rayPos = new Vector3(posObj.transform.position.x + direcDic[ControllerManager.instance.dPadDirection].x,
                      5,
                      posObj.transform.position.z + direcDic[ControllerManager.instance.dPadDirection].z);
        }

        Debug.DrawRay(rayPos, testvar, Color.cyan, Mathf.Infinity);


        if (Physics.Raycast(rayPos, Vector3.down, out var hit, 10, cloneLayer))
        {
            if (posObj == null) posObj = Instantiate(skillPosPrefab, this.transform.position, Quaternion.identity);
            Debug.Log(hit.collider.name);
            if (hit.collider.tag == "clone")
            {
                posObj.transform.position = hit.collider.transform.position;
            }
        }
        else
        {
            if (Physics.Raycast(firstRayPos, Vector3.down, out var hitA, 10, cloneLayer))
            {
                Debug.DrawRay(firstRayPos, testvar, Color.red, Mathf.Infinity);
                posObj.transform.position = hitA.collider.transform.position;
            }
        }

        await UniTask.Delay(250);
        smg.isPosSelectNow = false;
    }

}
