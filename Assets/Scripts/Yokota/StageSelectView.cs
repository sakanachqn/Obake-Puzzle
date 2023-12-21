using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectView : MonoBehaviour
{
    private Image[] stageImages = new Image[6];

    private StageImageView[] stageImageViews = new StageImageView[6];

    [SerializeField]
    private Image prefabImage;

    [SerializeField]
    private Image Board;

    // ステージ数
    [SerializeField]
    private int stageNum;

    public int StageNum => stageNum;

    private int leftUpSpriteNum = 1;

    private enum positionName
    {
        upperLeft = 0,
        upperMiddle,
        upperRight,
        lowerLeft,
        lowerMiddle,
        lowerRight
    }

    private Dictionary<positionName, Vector3> nameToVector3 = new Dictionary<positionName, Vector3>();

    private Dictionary<int, Sprite> stageSprites = new Dictionary<int, Sprite>();

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        nameToVector3.Add(positionName.upperLeft, new Vector3(-538, 94, 0));
        nameToVector3.Add(positionName.lowerLeft, new Vector3(-538, -238, 0));
        nameToVector3.Add(positionName.upperMiddle, new Vector3(0, 94, 0));
        nameToVector3.Add(positionName.lowerMiddle, new Vector3(0, -231, 0));
        nameToVector3.Add(positionName.upperRight, new Vector3(541, 94, 0));
        nameToVector3.Add(positionName.lowerRight, new Vector3(541, -231, 0));

        for (int i = 1; i <= stageNum; i++)
        {
            stageSprites.Add(i, Resources.Load<Sprite>("UI/Stage/Ui_005_1 " + i));
        }

        for (int i = 0; i < stageNum; i++)
        {
            stageImages[i] = Instantiate(prefabImage);
            stageImageViews[i] = stageImages[i].GetComponent<StageImageView>();
            stageImages[i].transform.SetParent(Board.transform, false);
            stageImages[i].rectTransform.position
                += nameToVector3[(positionName)i];
            stageImages[i].sprite = stageSprites[i + 1];
        }
    }

    public void SpriteChange(int key)
    {
        if (key > 0)
        {
            leftUpSpriteNum += 2;
            for (int i = 0; i < 6; i++)
            {
                stageImages[i].sprite = stageSprites[leftUpSpriteNum + i];
            }
        }
        else
        {
            leftUpSpriteNum -= 2;
            for (int i = 0; i < 6; i++)
            {
                stageImages[i].sprite = stageSprites[leftUpSpriteNum + i];
            }
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
