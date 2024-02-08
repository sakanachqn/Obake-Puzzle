using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;


public class SkillSuction : Skill
{
    public SkillSuction(SkillAreaDisplay skillAreaDisplay, SkillManager skillManager) : base(skillAreaDisplay, skillManager)
    {
    }

    // スキル発動のメソッド（非同期）
    public override async void SkillActivate()
    {
        // スキルが発動中であることを表すフラグをセット
        SkillManager.IsNowSkill = true;

        // 待機（ディレイ）
        await UniTask.Delay(1);

        // レイキャストで鉄箱を検出し、吸引する
        if (Physics.Raycast(sad.transform.position, sad.transform.forward, out var hit, 1) && sm.suctionObj == null)
        {
            if (hit.collider.tag == "IronBox")
            {
                ObakeAnimation.Inctance.SuctionAnimation();
                sm.suctionObj = hit.collider.gameObject;
                await ObjectMoveAnimation(sm.suctionObj, sm.transform.position, 0f);
                sm.suctionObj.SetActive(false);
                sm.plCon.plMove.isWalkCount = true;
            }
            else
            {
                ObakeAnimation.Inctance.SuctionMissAnimation();
            }
        }
        SkillManager.IsNowSkill = false;
    }

    // 吸引したオブジェクトを逆方向に放出するメソッド
    public async void ReverseObject()
    {
        // 吸引中であることを表すフラグをセット
        SkillManager.isNowSuction = true;
        var vec3 = sm.gameObject.transform.Find("Foward").transform.position;
        await UniTask.Delay(1);
        if (Physics.Raycast(sad.transform.position, sad.transform.forward, out var hit, 1))
        {
            ObakeAnimation.Inctance.SuctionMissAnimation();
        }
        else
        {
            vec3.x = Mathf.Round(vec3.x);
            vec3.z = Mathf.Round(vec3.z);
            if (vec3.x > 5 || vec3.x < 0 || vec3.z > 5 || vec3.z < 0)
            {
                ObakeAnimation.Inctance.SuctionMissAnimation();
            }
            else
            {
                ObakeAnimation.Inctance.SpittingoutAnimation();
                sm.suctionObj.transform.position = sm.gameObject.transform.position;
                sm.suctionObj.SetActive(true);
                await ObjectMoveAnimation(sm.suctionObj, vec3, 1.0f);
                sm.plCon.plMove.WalkCount = 0;
                sm.suctionObj = null;

        // 吸引中フラグをリセット
        SkillManager.isNowSuction = false;
    }

    // オブジェクトを動かすためのアニメーションメソッド（非同期）
    private async UniTask ObjectMoveAnimation(GameObject gobj, Vector3 endPos, float endSize)
    {
        gobj.transform.DOMove(endPos, 0.5f); // オブジェクトを移動
        await gobj.transform.DOScale(new Vector3(endSize, endSize, endSize), 0.5f); // オブジェクトのスケールを変更
        return;
    }
}
