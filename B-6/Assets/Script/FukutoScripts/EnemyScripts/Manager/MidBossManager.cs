using UnityEngine;

/// <summary>
/// ノーマルエネミーマネージャー
/// 
/// 中ボスの管理を行うクラス
/// </summary>
public class MidBossManager : NomalEnemyManager
{

    #region State
    WeaponSkill weaponSkill;
    #endregion

    private void Start()
    {
        InitValue();
    }

    private void Update()
    {
        ManageEnemy();
    }

    /// @brief 敵を管理する関数
    protected override void ManageEnemy()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Skill:
                Skill();
                break;
            case EnemyState.Hit:
                Hit();
                break;
            case EnemyState.Dead:
                Dead();
                break;
        }
    }

    /// @brief 初期化関数
    protected override void InitValue()
    {
        SetMovementScript();
        SetAnimationScript();
        enemyHealth = GetComponent<EnemyHealth>();
        shortAttack = GetComponent<ShortAttack>();
        weaponSkill = GetComponent<WeaponSkill>();
        enemyState = EnemyState.Idle;
    }

    /// @brief 待機状態関数
    protected override void Idle()
    {
        // 移動不可
        SetStopMovement(true);

        SetIdleAnimation();

        // 被ダメージへの遷移処理
        TransitionHit();

        // 対象に近づいていない場合
        if (!GetIsAttached())
        {
            Debug.Log("Moveに遷移します");
            enemyState = EnemyState.Move;
            ResetAnimation();
            return;
        }
        else if(shortAttack.GetIsIdle()) // 待機時の時だけ攻撃へ遷移
　      {
            enemyState = EnemyState.Attack;
            ResetAnimation();
            return;
        }
        else if(weaponSkill.GetIsIdle())
        {
            enemyState = EnemyState.Skill;
            ResetAnimation();
            return;
        }
        
    }

    /// @brief 移動状態関数
    protected override void Move()
    {
        // 移動可能
        SetStopMovement(false);

        SetMoveAnimation();

        // 被ダメージへの遷移処理
        TransitionHit();

        // 対象に近づいている場合
        if (GetIsAttached())
        {
            Debug.Log("Idleに遷移します");
            // 待機への遷移処理
            TransitionIdle();
        }
    }

    /// @brief 攻撃状態関数
    protected override void Attack()
    {
        // 移動不可
        SetStopMovement(true);

        // 被ダメージへの遷移処理
        TransitionHit();

        // 敵から離れている & イベントアニメーションが終了していたら
        if (!GetIsAttached() && FinishedEventAnimation())
        {
            // 一度待機に戻る
            enemyState = EnemyState.Idle;
            ResetAnimation();
            return;
        }

        // Idle状態だったら
        if (shortAttack.GetIsIdle())
        {
            ResetAnimation();
            SetAttackAnimation();
            shortAttack.SetStateAttack();
            return;
        }
        else if(shortAttack.GetIsRecast()) // リキャスト状態の場合
        {
            // 前のイベントアニメーション（攻撃やスキル）が終了していたら
            if (!FinishedEventAnimation()) return;

            // 一度待機に戻る
            enemyState = EnemyState.Idle;
            ResetAnimation();
            return;
        }
    }

    /// @brief スキル状態関数
    protected void Skill()
    {
        // 移動不可
        SetStopMovement(true);

        // 被ダメージへの遷移処理
        TransitionHit();

        // 敵から離れている & イベントアニメーションが終了していたら
        if (!GetIsAttached() && FinishedEventAnimation())
        {
            // 一度待機に戻る
            enemyState = EnemyState.Idle;
            ResetAnimation();
            return;
        }

        // Idle状態だったら
        if (weaponSkill.GetIsIdle())
        {
            ResetAnimation();
            SetSkillAnimation();
            weaponSkill.SetStateSkill();
            return;
        }
        else if (weaponSkill.GetIsRecast())
        {
            // 前のイベントアニメーション（攻撃やスキル）が終了していたら
            if (!FinishedEventAnimation()) return;

            // 一度待機に戻る
            enemyState = EnemyState.Idle;
            ResetAnimation();
            return;
        }
    }

    /// @brief 被ダメージ状態関数
    protected override void Hit()
    {
        // 移動不可
        SetStopMovement(true);

        //ダメージを受けている最中でもアニメーションを繰り返すため
        // 遷移処理を挟む
        TransitionHit();

        if (FinishedEventAnimation())
        {
            // 待機への遷移処理
            TransitionIdle();
        }
    }

    /// @brief 死亡状態関数
    protected override void Dead()
    {
        // 移動不可
        SetStopMovement(true);

        if (FinishedDeathAnimation())
        {
            // 死亡アニメーションが終了したら削除
            DeathProcess();
        }
    }

    protected override void BossHit()
    {
        bossMove.SetIsHit(true);
    }

    protected void SetSkillAnimation()
    {
        if (onMidBossAnimation) midBossAnimation.SetSkill();
    }
}
