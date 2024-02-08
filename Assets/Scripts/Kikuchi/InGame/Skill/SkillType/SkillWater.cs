using System.Collections;
using System.Collections.Generic;
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
                // 中略: 効果の発動処理（オブジェクトの移動、水のエフェクト、サウンド、アニメーションの再生など）

                // 効果発動中フラグをリセット
                SkillManager.IsNowEffect = false;
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
