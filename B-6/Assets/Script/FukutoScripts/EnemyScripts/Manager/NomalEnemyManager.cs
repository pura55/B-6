using UnityEngine;

/// <summary>
/// ノーマルエネミーマネージャー
/// 
/// 普通の敵を管理処理を行う基底クラス
/// </summary>
public class NomalEnemyManager : BaseEnemyManager
{
    private bool dead;
    private bool finishedAnimation = false;

    #region Config
    protected ShortAttack shortAttack; // 近接攻撃
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

        // 攻撃済みフラグがfalse & イベントアニメーションが終了していたら
        if (!shortAttack.GetIsAttacked() && FinishedEventAnimation())
        {
            SetAttackAnimation();
            shortAttack.SetAttackState();
            shortAttack.SetIsAttacked(true);
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

        if (GetHitRock() && FinishedEventAnimation())
        {
            enemyState = EnemyState.Dead;
            ResetAnimation();
            return;
        }
        else if(FinishedEventAnimation())
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

        if(finishedAnimation)
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
        if (isTakeHit || GetHitRock())
        {
            Debug.Log("Hitに遷移します");
            enemyState = EnemyState.Hit;
            isTakeHit = false;
            ResetAnimation();
            SetHitAnimation();
            return;
        }
    }

    protected bool GetHitRock()
    {
        return enemyHealth.GetHitRock();
    }
}
