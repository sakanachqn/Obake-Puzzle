using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;

public class SkillUIChange : MonoBehaviour
{

    private static SkillUIChange instance;
    public static SkillUIChange Instance => instance;

    //スキルUI保管場所
    [Header("1つ目:炎スキル、2つ目:水スキル、3つ目:吸い込みスキル")]
    [SerializeField]
    private List<Sprite> buttonXUI;
    [SerializeField]
    private List<Sprite> buttonYUI;

    [Header("スキルUIを入れるボタン")]
    [SerializeField]
    private Image buttonX;
    [SerializeField]
    private Image buttonY;

    [SerializeField]
    private TextMeshProUGUI xCount;
    [SerializeField]
    private TextMeshProUGUI yCount;

    //スキル使用回数
    [HideInInspector]
    public int buttonXCount;
    [HideInInspector]
    public int buttonYCount;

    private CSVMapGenerate CSVMapGenerate;

    private SkillManager sm;

    async void Start()
    {
        if(instance == null) instance = this;
        CSVMapGenerate = CSVMapGenerate.Instance;
        await UniTask.WaitUntil(() => CSVMapGenerate.IsMapGenerate);
        sm = PlayerController.PlayerObject.GetComponent<SkillManager>();

        IconChange();

    }

    private void Update()
    {
        if (sm == null)
        {
            sm = PlayerController.PlayerObject.GetComponent<SkillManager>();
            IconChange();
        }
        yCount.text = sm.skillOneLim.ToString("F0");
        xCount.text = sm.skillTwoLim.ToString("F0");
    }

    private void OnDestroy()
    {
        instance = null;
    }

    private void IconChange()
    {

        int i = 0;
        //CSVのスキルの名前からUIに対応したスキル画像を差し込む
        foreach (var name in CSVMapGenerate.SkillName)
        {
            switch (name)
            {
                //炎スキル
                case "F":
                    if (i == 0) buttonX.sprite = buttonXUI[0];
                    else buttonY.sprite = buttonYUI[0];
                    break;
                //水スキル
                case "W":
                    if (i == 0) buttonX.sprite = buttonXUI[1];
                    else buttonY.sprite = buttonYUI[1];
                    break;
                //吸い込みスキル
                case "S":
                    if (i == 0) buttonX.sprite = buttonXUI[2];
                    else buttonY.sprite = buttonYUI[2];
                    break;
            }

            i++;
        }
    }
}
