using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // コントローラーマネージャーの参照
    public ControllerManager ctrl;

    // プレイヤーコントローラーの参照
    public PlayerController plCon;

    // スキル範囲表示のためのSkillAreaDisplayクラスのインスタンス
    private SkillAreaDisplay skillArea;

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
    [SerializeField]
    private skillType skillOneType;
    [SerializeField]
    private skillType skillTwoType;

    // スキルのタイプを定義するenum
    private enum skillType
    {
        skillA,
        skillB,
        skillC,
        Null
    };

    // 押されたボタンの種類判別用の変数
    private skillType selectSkill = skillType.Null;


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
        // 各スキルクラスを変数に格納
        currentSkillA = new SkillWater(skillArea, this);
        currentSkillB = new SkillFire(skillArea, this);
        currentSkillC = new SkillSuction(skillArea, this);

    }


    public void SkillManagerUpdate()
    {
        if (ctrl.CtrlInput.Skill.SkillA.WasPressedThisFrame())
        {
            if(!IsNowSkill)
            {
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

    }

    private void SkillProcess()
    {
        if (skillArea.areaViewNow)
        {
            if (ctrl.CtrlInput.Skill.Cancel.WasPressedThisFrame()) skillArea.HideSkillArea();
            // コントローラーマネージャーのdPadSkillDirectionがNullでない場合
            if (ControllerManager.instance.dPadDirection != ControllerManager.Direction.Null && !isPosSelectNow)
            {
                skillArea.GetSkillPos();
            }
            if(ctrl.CtrlInput.Skill.Select.WasPressedThisFrame() && selectSkill == skillType.skillA)
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
