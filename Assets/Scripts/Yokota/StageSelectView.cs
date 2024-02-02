using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectView : MonoBehaviour
{
    private List<GameObject> stageObj = new List<GameObject>();

    private List<Image> imageList = new List<Image>();

    [SerializeField]
    private List<StageImageView> stageImageViews;

    [SerializeField]
    private List<GameObject> prefabImages;

    [SerializeField]
    private Canvas canvas;

    // ステージ数
    [SerializeField]
    private int stageNum;

    public int StageNum => stageNum;

    public bool isMoving = false;

    private Vector3 startSetPosition = new Vector3(325f, 590f);

    private float positionIntervalX = 325f;
    private float positionIntervalY = 390f;

    private Dictionary<int, Sprite> stageSprites = new Dictionary<int, Sprite>();

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        for (int i = 1; i <= stageNum; i++)
        {
            stageSprites.Add(i, Resources.Load<Sprite>("UI/Stage/Ui_005_1 " + i));
        }

        for (int i = 0; i < stageNum; i++)
        {
            stageObj.Add(Instantiate(prefabImages[i % 6]));
            var children = stageObj[i].GetComponentsInChildren<Image>();
            imageList.Add(children[1]);
            stageImageViews.Add(stageObj[i].GetComponentInChildren<StageImageView>());
            stageObj[i].transform.SetParent(canvas.transform, false);

            if (i == 0) 
            {
                stageObj[i].transform.position = startSetPosition;
            }
            else if (i % 2 == 0)
            {
                stageObj[i].transform.position = stageObj[i - 1].transform.position + new Vector3(positionIntervalX, positionIntervalY, 0);
            }
            else
            {
                stageObj[i].transform.position = stageObj[i - 1].transform.position + new Vector3(positionIntervalX, -positionIntervalY, 0);
            }

            imageList[i].sprite = stageSprites[i + 1];
        }
    }

    
    public void MoveLeftOrRight(float key)
    {
        isMoving = true;

        for (int i = 0; i < stageObj.Count; i++)
        {
            stageObj[i].transform.DOMoveX(key * -325f, 1f).
                SetRelative(true).
                OnComplete(() => isMoving = false) ;
        }
    }

    public void BrightUp(int CursorPos)
    {
        for (int i = 0; i < stageNum; i++)
        {
            if (i == CursorPos) stageImageViews[i].matchCursor = true;
            else stageImageViews[i].matchCursor = false;
        }
    }
}
