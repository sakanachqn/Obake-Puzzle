using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using UnityEditor.ShaderKeywordFilter;

public class MoveCloud : MonoBehaviour
{
    [SerializeField]
    private float time = 0;
    [SerializeField]
    private Vector3 pos;
    // Start is called before the first frame update
    //async void Start()
    //{
    //    //await this.transform.DOMove(pos, time).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    //}

    private void Update()
    {
        this.transform.Rotate(Vector3.up);
    }




}
