using UnityEngine;

/// <summary>
/// ノーマルエネミーマネージャー
/// 
/// 普通の敵の管理を行うクラス
/// </summary>
public class NomalEnemyManager : BaseEnemyManager
{
    
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
        enemyState = EnemyState.Idle;
    }

    /// @brief 待機状態関数
    protected override void Idle()
    {
        // 落石ヒット時の遷移処理
        if (GetHitRock())
        {
            SetIsDead();
        }

        if (TransitionDead())
        {
            return;
        }
        // 移動不可
        SetStopMovement(true);

        SetIdleAnimation();

        // 落石ヒット時の遷移処理
        if (GetHitRock())
        {
            if (TransitionDead())
            {
                return;
            }
        }

        // 被ダメージへの遷移処理
        TransitionHit();

        // 対象に近づいていない場合
        if (!GetIsAttached())
        {
            //Debug.Log("Moveに遷移します");
            enemyState = EnemyState.Move;
            ResetAnimation();
            return;
        }
        else
        {
            enemyState = EnemyState.Attack;
            ResetAnimation();
            return;
        }
    }

    /// @brief 移動状態関数
    protected override void Move()
    {
        // 落石ヒット時の遷移処理
        if (GetHitRock())
        {
            SetIsDead();
        }

        if (TransitionDead())
        {
            return;
        }

        // 移動可能
        SetStopMovement(false);

        SetMoveAnimation();


        // 被ダメージへの遷移処理
        TransitionHit();

        // 対象に近づいている場合
        if (GetIsAttached())
        {
            //Debug.Log("Idleに遷移します");
            // 待機への遷移処理
            TransitionIdle();
        }
    }

    /// @brief 攻撃状態関数
    protected override void Attack()
    {
        // 落石ヒット時の遷移処理
        if (GetHitRock())
        {
            SetIsDead();
        }

        // 死亡への遷移処理
        if (TransitionDead())
        {
            return;
        }

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

        // idle状態だったら
        if (shortAttack.GetIsIdle())
        {
            ResetAnimation();
            SetAttackAnimation();
            shortAttack.SetStateAttack();
            return;
        }
    }

    /// @brief 被ダメージ状態関数
    protected override void Hit()
    {
        if (TransitionDead())
        {
            return;
        }

        // 移動不可
        SetStopMovement(true);

        // 落石ヒット時の遷移処理
        if (GetHitRock())
        {
            if (TransitionDead())
            {
                return;
            }
        }

        //ダメージを受けている最中でもアニメーションを繰り返すため
        // 遷移処理を挟む
        TransitionHit();

        if(FinishedEventAnimation())
        {
            if (TransitionDead())
            {
                return;
            }
            // 待機への遷移処理
            TransitionIdle();
            return;
        }
    }

    /// @brief 死亡状態関数
    protected override void Dead()
    {
        // 移動不可
        SetStopMovement(true);

        if(FinishedDeathAnimation())
        {
            // 死亡アニメーションが終了したら削除
            DeathProcess();
        }
    }

    /// @brief 待機への遷移処理を行う関数
    protected void TransitionIdle()
    {
        enemyState = EnemyState.Idle;
        ResetAnimation();
        return;
    }

    /// @brief 被ダメージへの遷移処理を行う関数
    protected void TransitionHit()
    {
        // ダメージ受けたら
        if (isTakeHit)
        {
            Debug.Log("Hitに遷移します");
            enemyState = EnemyState.Hit;
            isTakeHit = false;
            shortAttack.SetStateRecast();
            ResetAnimation();
            SetHitAnimation();
            BossHit();
            return;
        }
    }

    protected virtual void BossHit()
    {
    }

    /// @brief 死亡への遷移処理を行う関数
    protected bool TransitionDead()
    {
        if(!isDead)
        {
            return false;
        }

        //Debug.Log("Deadに遷移します");
        enemyState = EnemyState.Dead;
        ResetAnimation();
        SetDeathAnimation();
        return true;
    }

    protected bool GetHitRock()
    {
        return enemyHealth.GetHitRock();
    }
}
