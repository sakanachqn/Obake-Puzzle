using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class SkillWater : Skill
{
    // EffectInstanceの参照を保持するための変数
    EffectInstance effectInstance;

    // コンストラクタ
    public SkillWater(SkillAreaDisplay skillAreaDisplay, SkillManager skillManager) : base(skillAreaDisplay, skillManager)
    {
        // EffectInstanceを取得し、effectInstance変数に格納
        effectInstance = sad.gameObject.GetComponent<EffectInstance>();
    }

    // スキル発動のメソッド（非同期）
    public override async void SkillActivate()
    {
        if (sad.posObj == null) return;
        // スキルが発動中であることを表すフラグをセット
        SkillManager.IsNowSkill = true;

        // スキルエリアの位置を取得
        Vector3 pos = sad.posObj.transform.position;
        if (pos.y > 1) pos.y = 1;

        // スキルエリアを非表示にする
        sad.HideSkillArea();

        // 効果発動中であることを表すフラグをセット
        SkillManager.IsNowEffect = true;

        // 待機（ディレイ）
        await UniTask.Delay(1);

        // レイキャストで鉄箱または木箱を検出し、水の効果を発動する
        if (Physics.Raycast(sad.transform.position, pos - sad.transform.position, out var hit, 1))
        {
            if (hit.collider.tag == "IronBox" || hit.collider.tag == "WoodenBox")
            {
                var direc = hit.transform.position - sad.transform.position;
                var objBack = hit.transform.position + direc;
                if (0 > objBack.x || 0 > objBack.z || 4 < objBack.x || 4 < objBack.z) return;
                if (Physics.Raycast(hit.transform.position, direc, out var hitTwo, 1)) return;
                effectInstance.WaterEffect(sad.gameObject.transform.position, direc);
                await UniTask.Delay(500);
                SoundManager.Instance.Play("SEWater");
                ObakeAnimation.Inctance.WaterAnimation();
                await UniTask.Delay(1000);
                await hit.transform.DOMove(hit.transform.position + direc, 1);
                await UniTask.Delay(1000);
                SkillManager.IsNowEffect = false;
                if (sm.skillOneType == SkillManager.skillType.skillA) sm.skillOneLim--;
                if (sm.skillTwoType == SkillManager.skillType.skillA) sm.skillTwoLim--;
            }
            else
            {
                // 効果発動中フラグをリセット
                SkillManager.IsNowEffect = false;
                // 水のスキルがミスした場合のアニメーションを再生
                ObakeAnimation.Inctance.WaterMissAnimation();
            }
        }
        else
        {
            // 効果発動中フラグをリセット
            SkillManager.IsNowEffect = false;
            // 水のスキルがミスした場合のアニメーションを再生
            ObakeAnimation.Inctance.WaterMissAnimation();
        }
    }
}
