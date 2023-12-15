using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem.HID;

public class MapObjectUtil : MonoBehaviour
{
    private bool isDropNow = false;
    private bool isMoveNow = false;

    private Vector3 tempPos;

    //public GameObject OnTop = null;
    //public GameObject OnBottom = null;

    private void Start()
    {
        tempPos = transform.position;

        //if(Physics.Raycast(this.transform.position, Vector3.up, out var hitTop, 1))
        //{
        //    OnTop = hitTop.collider.gameObject;
        //}
        //if (Physics.Raycast(this.transform.position, Vector3.down, out var hitBottom, 1))
        //{
        //    if(hitBottom.collider.tag == "MapTile" || hitBottom.collider.tag == "Pitfall")
        //    OnBottom = hitBottom.collider.gameObject;
        //}

    }
    // Update is called once per frame
    void Update()
    {
        
        if (this.transform.position != tempPos)
        {
            isMoveNow = true;
        }
        else isMoveNow = false;

        if(!SkillManager.IsNowSkill && !PlayerController.IsNowAction && !isDropNow && !isMoveNow)
        {
            if(Physics.Raycast(this.transform.position, Vector3.down, out var hit, 1))
            {
                if (hit.collider.tag == "Pitfall") DropObject();

            }
            else DropObject();
        }

        tempPos = transform.position;
    }

    private async void DropObject()
    {
        isDropNow = true;
        await this.transform.DOMove(this.transform.position + Vector3.down, 1);
        if (Physics.Raycast(this.transform.position, Vector3.down, out var hit, 1))
        {
            if (hit.collider.tag == "ironBox" || hit.collider.tag == "WoodenBox" || hit.collider.tag == "MapTile")
            {
                isDropNow = false;
            }
        }
    }

    //private void OnDestroy()
    //{
    //    if(OnBottom.GetComponent<MapObjectUtil>() != null) OnBottom.GetComponent<MapObjectUtil>().OnTop = null;
    //}

}
