using UnityEngine;

/// <summary>
/// ショートスキル
/// 
/// 近接スキルのクラス
/// </summary>
public class ShortSkill : BaseEnemySkill
{
    #region State
    protected HitTriggerManager hitTrigger; // 当たり判定
    #endregion

    private void Start()
    {
        InitValue();
    }

    private void Update()
    {
        ManageAttacking();
    }

    protected override void ManageAttacking()
    {
        switch (skillState)
        {
            case EnemySkillState.idle:
                break;
            case EnemySkillState.skill:
                UsingSkill();
                break;
            case EnemySkillState.recast:
                Recast();
                break;
        }
    }

    protected override void UsingSkill()
    {
        if (midBossAnimation.GetSpriteIndex() == hitAnimationNumber)
        {
            // ヒットボックスオン
            ActiveHitBox();
            isAttacked = true;
        }
        else if (isAttacked)
        {
            // ヒットボックスオフ
            InactiveHitBox();
            // 状態をリキャストへ遷移
            skillState = EnemySkillState.recast;
        }
        else
        {
            // ヒットボックスオフ
            InactiveHitBox();
        }
    }

    protected override void InitValue()
    {
        statSkill = enemyProgressData.GetIntStat(enemyID, skillStatName);
        recastInterval = enemyProgressData.GetFloatStat(enemyID, intervalStatName);
        midBossAnimation = gameObject.GetComponent<MidBossAnimation>();
        hitTrigger = transform.GetChild(0).gameObject.GetComponent<HitTriggerManager>();
        skillState = EnemySkillState.idle;
    }

    protected override void Recast()
    {
        if (isAttacked)
            CompleteInterval();
        else
        {
            skillState = EnemySkillState.idle;
            return;
        }
    }

    /// @brief 当たり判定をアクティブにする関数
    protected void ActiveHitBox()
    {
        hitTrigger.SetHitTrigger(true);
    }

    /// @brief 攻撃フラグを非アクティブにする関数
    protected void InactiveHitBox()
    {
        hitTrigger.SetHitTrigger(false);
    }
}
