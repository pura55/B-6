using UnityEditor.Tilemaps;
using UnityEngine;

/// <summary>
/// ショートディスタンスアタック
/// 
/// 敵の近接攻撃処理を行うクラス
/// </summary>
public class ShortAttack : BaseEnemyAttack
{
    #region State
    [SerializeField] protected bool onNomalAnimation = false; // インスペクターにNomalAnimationがあるかどうか
    [SerializeField] protected bool onIncludeMovementAnimation = false; // インスペクターにIncludeMovementAnimationがあるかどうか
    [SerializeField] protected bool onMidBossAnimation = false;
    NomalAnimation nomalAnimation;
    IncludeMovementAnimation movementAnimation;
    MidBossAnimation midBossAnimation;
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitValue();
    }

    // Update is called once per frame
    void Update()
    {
        ManageAttacking();
    }

    protected override void ManageAttacking()
    {
        switch (attackState)
        {
            case EnemyAttackState.idle:
                break;
            case EnemyAttackState.attacking:
                Attacking();
                break;
            case EnemyAttackState.recast:
                Recast();
                break;
        }
    }

    /// @brief 変数を初期化する関数
    protected override void InitValue()
    {
        statAtk = enemyProgressData.GetIntStat(enemyID, attackStatName);
        recastInterval = enemyProgressData.GetFloatStat(enemyID, intervalStatName);
        attackState = EnemyAttackState.idle;
        hitTrigger = transform.GetChild(0).gameObject.GetComponent<HitTriggerManager>();
        SetAnimationScript();
    }

    /// @brief 攻撃を管理する関数
    protected override void Attacking()
    {
        if (onNomalAnimation)
        {
            // スプライトの指数が攻撃ヒット時の番号と一致している場合
            if(nomalAnimation.GetSpriteIndex() == hitAnimationNumber)
            {
                // ヒットボックスオン
                ActiveHitBox();
                isAlreadyHit = true;
            }
            else if(isAlreadyHit)
            {
                // ヒットボックスオフ
                InactiveHitBox();
                // 状態をリキャストへ遷移
                attackState = EnemyAttackState.recast;
            }
        }
        else if (onIncludeMovementAnimation)
        {
            // スプライトの指数が攻撃ヒット時の番号と一致している場合
            if (movementAnimation.GetSpriteIndex() == hitAnimationNumber)
            {
                // ヒットボックスオン
                ActiveHitBox();
                isAlreadyHit = true;
            }
            else if (isAlreadyHit)
            {
                // ヒットボックスオフ
                InactiveHitBox();
                // 状態をリキャストへ遷移
                attackState = EnemyAttackState.recast;
            }
        }
        else if(onMidBossAnimation)
        {
            // スプライトの指数が攻撃ヒット時の番号と一致している場合
            if (midBossAnimation.GetSpriteIndex() == hitAnimationNumber)
            {
                // ヒットボックスオン
                ActiveHitBox();
                isAlreadyHit = true;
            }
            else if (isAlreadyHit)
            {
                // ヒットボックスオフ
                InactiveHitBox();
                // 状態をリキャストへ遷移
                attackState = EnemyAttackState.recast;
            }
        }
    }

    /// @brief リキャスト処理を行う関数
    protected override void Recast()
    {
        if (isAttacked)
            CompleteInterval();
        else
            attackState = EnemyAttackState.idle;
    }

    /// @brief アニメーションスクリプトを設定する関数
    protected void SetAnimationScript()
    {
        // NomalAnimationeのコンポーネントの取得
        if (onNomalAnimation) nomalAnimation = GetComponent<NomalAnimation>();

        // IncludeMovementAnimationのコンポーネントの取得
        else if (onIncludeMovementAnimation) movementAnimation = GetComponent<IncludeMovementAnimation>();

        // MidBossAnimationのコンポーネントの取得
        else if(onMidBossAnimation) midBossAnimation = GetComponent<MidBossAnimation>();
    }

}
