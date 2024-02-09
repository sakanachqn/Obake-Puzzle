using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // コントローラーマネージャーの参照
    public ControllerManager ctrl;

    // プレイヤーコントローラーの参照
    public PlayerController plCon;

    // スキル範囲表示のためのSkillAreaDisplayクラスのインスタンス
    private SkillAreaDisplay skillArea;

    public CSVMapGenerate csvGene;

    private SkillUIChange ui;

    // 吸引オブジェクトの参照
    [SerializeField]
    public GameObject suctionObj;

    // スキルが発動中かどうかのフラグ
    public static bool IsNowSkill = false;
    public static bool isNowSuction = false;
    public static bool IsNowEffect = false;

    // スキル発動位置選択中かどうかのフラグ
    public bool isPosSelectNow = false;

    // 各スキルのクラスインスタンス
    public Skill currentSkillA;
    public Skill currentSkillB;
    private SkillSuction currentSkillC;
    public SkillSuction CurrentSkillC => currentSkillC; 

    // スキルタイプの定義
    public skillType skillOneType;
    public skillType skillTwoType;

    // スキルのタイプを定義するenum
    public enum skillType
    {
        skillA,
        skillB,
        skillC,
        Null
    };

    // 押されたボタンの種類判別用の変数
    private skillType selectSkill = skillType.Null;

    public int skillOneLim = 0;
    public int skillTwoLim = 0;

    public void SkillManagerStart()
    {
        IsNowSkill = false;
        isNowSuction = false;
        IsNowEffect = false;
        suctionObj = null;

        // コントローラーマネージャーの取得
        ctrl = ControllerManager.instance;
        // プレイヤーコントローラーの取得
        plCon = GetComponent<PlayerController>();
        // スキル範囲表示クラス取得
        skillArea = GetComponent<SkillAreaDisplay>();

        
        csvGene = CSVMapGenerate.Instance;
        // 各スキルクラスを変数に格納
        currentSkillA = new SkillWater(skillArea, this);
        currentSkillB = new SkillFire(skillArea, this);
        currentSkillC = new SkillSuction(skillArea, this);

        if(csvGene != null)
        {
            switch (csvGene.SkillName[0])
            {
                case "W":
                    {
                        skillOneType = skillType.skillA;
                        break;
                    }
                case "F":
                    {
                        skillOneType = skillType.skillB;
                        break;
                    }
                case "S":
                    {
                        skillOneType = skillType.skillC;
                        break;
                    }
            }
            skillOneLim = csvGene.SkillCastLimit[0];
            switch (csvGene.SkillName[1])
            {
                case "W":
                    {
                        skillTwoType = skillType.skillA;
                        break;
                    }
                case "F":
                    {
                        skillTwoType = skillType.skillB;
                        break;
                    }

                case "S":
                    {
                        skillTwoType = skillType.skillC;
                        break;
                    }
            }
            skillTwoLim = csvGene.SkillCastLimit[1];

        }

    }


    public void SkillManagerUpdate()
    {
        if (ctrl.CtrlInput.Skill.SkillA.WasPressedThisFrame())
        {
            if(!IsNowSkill)
            {
                if (skillOneLim == 0)
                {
                    //SE
                    return;
                }
                IsNowSkill = true;
                switch(skillOneType)
                {
                    case skillType.skillA:
                        skillArea.ShowSkillArea();
                        break;
                    case skillType.skillB:
                        skillArea.ShowSkillArea(true);
                        break;
                    case skillType.skillC:
                        currentSkillC.SkillActivate();
                        break;
                }
                selectSkill = skillOneType;
            }
        }
        if (ctrl.CtrlInput.Skill.SkillB.WasPressedThisFrame())
        {
            if (skillTwoLim == 0)
            {
                //SE
                return;
            }
            if (!IsNowSkill)
            {
                IsNowSkill = true;
                switch (skillTwoType)
                {
                    case skillType.skillA:
                        skillArea.ShowSkillArea();
                        break;
                    case skillType.skillB:
                        skillArea.ShowSkillArea(true);
                        break;
                    case skillType.skillC:
                        currentSkillC.SkillActivate();
                        break;
                }
                selectSkill = skillTwoType;
            }
        }
        if (IsNowSkill)
        {
            SkillProcess();
        }

        SkillUIChange.Instance.buttonXCount = skillOneLim;
        SkillUIChange.Instance.buttonYCount = skillTwoLim;
    }

    private void SkillProcess()
    {
        if (skillArea.areaViewNow)
        {
            if (ctrl.CtrlInput.Skill.Cancel.WasPressedThisFrame()) skillArea.HideSkillArea();
            // コントローラーマネージャーのdPadSkillDirectionがNullでない場合
            if (ControllerManager.instance.dPadDirection != ControllerManager.Direction.Null && !isPosSelectNow)
            {
                skillArea.GetSkillPos("DPad");
            }
            if (ControllerManager.instance.stickPlayerDirection != ControllerManager.Direction.Null && !isPosSelectNow)
            {
                skillArea.GetSkillPos("Stick");
            }
            if (ctrl.CtrlInput.Skill.Select.WasPressedThisFrame() && selectSkill == skillType.skillA)
            {
                currentSkillA.SkillActivate();
            }
            if (ctrl.CtrlInput.Skill.Select.WasPressedThisFrame() && selectSkill == skillType.skillB)
            {
                currentSkillB.SkillActivate();
            }

        }
    }

}
