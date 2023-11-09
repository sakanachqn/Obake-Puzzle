using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField]
    private Transform fireArea;
    [SerializeField]
    private GameObject areaFade;
    [SerializeField]
    private float areaFadePos = 0.505f;

    private ControllerManager ctrl;

    private List<Transform> fireSkillArea = new List<Transform>();

    private Dictionary<GameObject, Material> hitArea = new Dictionary<GameObject, Material>();

    private bool isNowSkill = false;

    private Material mat;
    [SerializeField]
    private Material changeMat;

    [SerializeField]
    private float fadeTime = 1;
    [SerializeField]
    private float underAlpha = 0.3f;
    [SerializeField]
    private GameObject gobj;


    private void Start()
    {
        ctrl = ControllerManager.instance;
        var index = fireArea.childCount;
        Debug.Log(index);
        for (int i = 0; i < index; i++)
        {
            fireSkillArea.Add(fireArea.GetChild(i));
            Debug.Log(fireSkillArea[i].name);
        }
        var token = this.GetCancellationTokenOnDestroy();


    }   

    public async UniTask FireSkill(CancellationToken token)
    {
        while (true)
        {
            if (isNowSkill) return;
            await UniTask.WaitUntil(() => ctrl.CtrlInput.Player.SkillA.WasPressedThisFrame(), cancellationToken: token);
            isNowSkill = true;
            var parent = new GameObject("Area");
            foreach (Transform t in fireSkillArea)
            {
                if (Physics.Raycast(t.position, Vector3.down, out var hit, Mathf.Infinity))
                {
                    if (hit.collider.tag == "Pitfall") return;
                    if (hit.transform.position.y != 0)
                    {
                        var a = hit.collider.gameObject.GetComponent<MeshRenderer>();
                        mat = a.material;
                        hitArea.Add(hit.collider.gameObject, mat);
                        a.material = changeMat;
                        a.material.DOFade(underAlpha, fadeTime).SetLoops(-1, LoopType.Yoyo);
                    }
                    if(hit.transform.position.y == 0)
                    {
                        Vector3 pos = hit.transform.position;
                        pos.y += 1;
                        var temp = Instantiate(gobj, pos, Quaternion.identity);
                        var tempMat = temp.GetComponent<MeshRenderer>();
                        hitArea.Add(temp, tempMat.material);
                        tempMat.material = changeMat;
                        tempMat.material.DOFade(underAlpha, fadeTime).SetLoops(-1, LoopType.Yoyo);
                    }
                }
            }
            await UniTask.WaitUntil(() => ctrl.CtrlInput.Player.Cancel.WasPressedThisFrame(), cancellationToken: token);
            foreach (KeyValuePair<GameObject, Material> kvp in hitArea)
            {
                kvp.Key.GetComponent<MeshRenderer>().material = kvp.Value;
                if(kvp.Key.tag == "clone")Destroy(kvp.Key.gameObject);
                
            }
            hitArea.Clear();
            isNowSkill = false;
        }
    }
}
