using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class SkillFire : Skill
{
    // EffectInstanceの参照を保持するための変数
    private EffectInstance effectInstance;

    // コンストラクタ
    public SkillFire(SkillAreaDisplay skillAreaDisplay, SkillManager skillManager) : base(skillAreaDisplay, skillManager)
    {
        // EffectInstanceを取得し、effectInstance変数に格納
        effectInstance = sad.gameObject.GetComponent<EffectInstance>();
    }

    // スキル発動のメソッド（非同期）
    public override async void SkillActivate()
    {
        // スキルが発動中であることを表すフラグをセット
        SkillManager.IsNowSkill = true;

        // スキルエリアの位置を取得
        var pos = sad.posObj.transform.position;
        if (pos.y > 1) pos.y = 1;

        // スキルエリアを非表示にする
        sad.HideSkillArea();

        // 効果発動中であることを表すフラグをセット
        SkillManager.IsNowEffect = true;

        // 待機（ディレイ）
        await UniTask.Delay(1);

        // 周囲の木箱を検出するためのリスト
        var boxs = new List<GameObject>();

        // すべての方向にレイキャストを発射し、木箱があればリストに追加
        foreach (KeyValuePair<ControllerManager.Direction, Vector3> kvp in sm.plCon.plMove.Directions)
        {
            if (Physics.Raycast(pos, kvp.Value, out var hit, 1))
            {
                if (hit.collider.tag == "WoodenBox")
                {
                    boxs.Add(hit.collider.gameObject);
                }
            }
        }

        // 木箱が1つ以上ある場合
        if (boxs.Count > 0)
        {
            // 火のスキルに関連するサウンドを再生
            SoundManager.Instance.Play("SEFire");
            // 炎のアニメーションを再生
            ObakeAnimation.Inctance.FlameAnimation();

            // 待機（ディレイ）
            await UniTask.Delay(100);

            // 炎のエフェクトを発生
            effectInstance.FireEffect(pos);

            // 待機（ディレイ）
            await UniTask.Delay(1000);

            // すべての木箱を破壊
            foreach (GameObject box in boxs)
            {
                Destroy(box);
            }

            // 待機（ディレイ）
            await UniTask.Delay(500);
            if (sm.skillOneType == SkillManager.skillType.skillB) sm.skillOneLim--;
            if (sm.skillTwoType == SkillManager.skillType.skillB) sm.skillTwoLim--;

            // 効果発動中フラグをリセット
            SkillManager.IsNowEffect = false;
        }
        else
        {
            // 木箱がない場合、失敗のアニメーションを再生
            ObakeAnimation.Inctance.FlameMissAnimation();
            // 効果発動中フラグをリセット
            SkillManager.IsNowEffect = false;
        }
    }
}


