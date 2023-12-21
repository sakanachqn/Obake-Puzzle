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
    private Image BackGround;

    // ステージ数
    [SerializeField]
    private int stageNum;

    public int StageNum => stageNum;

    private int pageNum;

    private int nowPageNum;

    private enum positionName
    {
        upperLeft = 0,
        lowerLeft,
        upperMiddle,
        lowerMiddle,
        upperRight,
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
        nameToVector3.Add(positionName.upperLeft, new Vector3(-550, 185, 0));
        nameToVector3.Add(positionName.lowerLeft, new Vector3(-550, -185, 0));
        nameToVector3.Add(positionName.upperMiddle, new Vector3(0, 185, 0));
        nameToVector3.Add(positionName.lowerMiddle, new Vector3(0, -185, 0));
        nameToVector3.Add(positionName.upperRight, new Vector3(550, 185, 0));
        nameToVector3.Add(positionName.lowerRight, new Vector3(550, -185, 0));

        pageNum = stageNum / 6;

        nowPageNum = 0;

        for (int i = 0; i < stageNum; i++)
        {
            stageSprites.Add(i, Resources.Load<Sprite>("Images/Yokota/TestImage" + (i + 1)));
        }

        for (int i = 0; i < 6; i++)
        {
            stageImages[i] = Instantiate(prefabImage);
            stageImageViews[i] = stageImages[i].GetComponent<StageImageView>();
            stageImages[i].transform.SetParent(BackGround.transform, false);
            stageImages[i].rectTransform.position
                += nameToVector3[(positionName)i];
            stageImages[i].sprite = stageSprites[i];
        }
    }

    public bool PageUp()
    {
        if (nowPageNum == pageNum) return false;

        nowPageNum++;

        int remainingStage = stageNum - 6;

        for (int i = 0; i < 6; i++)
        {
            if (remainingStage > i)
                stageImages[i].sprite = stageSprites[nowPageNum * 6 + i];
            else
                stageImages[i].gameObject.SetActive(false);
        }

        return true;
    }

    public bool PageDown()
    {
        if (nowPageNum == 0) return false;

        nowPageNum--;

        for (int i = 0; i < 6; i++)
        {
            stageImages[i].gameObject.SetActive(true);
            stageImages[i].sprite = stageSprites[nowPageNum * 6 + i];
        }

        return true;
    }

    public void BrightUp(int CursorPos)
    {
        for (int i = 0; i < 6; i++)
        {
            if (i == CursorPos) stageImageViews[i].matchCursor = true;
            else stageImageViews[i].matchCursor = false;
        }
    }
}
