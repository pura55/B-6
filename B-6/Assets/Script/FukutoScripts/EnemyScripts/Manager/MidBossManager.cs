using UnityEngine;

/// <summary>
/// ノーマルエネミーマネージャー
/// 中ボスの管理を行うクラス
/// </summary>
public class MidBossManager : NomalEnemyManager
{
    #region Config
    [SerializeField] private int bossID;
    #endregion

    #region State
    [Header("SKILL")]
    [SerializeField] private bool isSkillShort = false;
    private WeaponSkill weaponSkill;
    private ShortSkill shortSkill;
    #endregion

    private void Start()
    {
        InitValue();
    }

    private void Update()
    {
        ManageEnemy();
    }

    /// <summary>
    /// 敵を管理する
    /// </summary>
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

    /// <summary>
    /// 初期化
    /// </summary>
    protected override void InitValue()
    {
        SetMovementScript();
        SetAnimationScript();
        enemyHealth = GetComponent<EnemyHealth>();
        shortAttack = GetComponent<ShortAttack>();

        if (isSkillShort)
        {
            shortSkill = GetComponent<ShortSkill>();
        }
        else
        {
            weaponSkill = GetComponent<WeaponSkill>();
        }

        enemyState = EnemyState.Idle;
    }

    /// <summary>
    /// 待機状態
    /// </summary>
    protected override void Idle()
    {
        if (TransitionDead()) return;

        SetStopMovement(true);
        SetIdleAnimation();

        if (!GetIsAttached())
        {
            Debug.Log("Moveに遷移します");
            enemyState = EnemyState.Move;
            ResetAnimation();
            return;
        }
        else if (shortAttack.GetIsIdle())
        {
            enemyState = EnemyState.Attack;
            ResetAnimation();
            return;
        }
        else if (isSkillShort ? shortSkill.GetIsIdle() : weaponSkill.GetIsIdle())
        {
            enemyState = EnemyState.Skill;
            ResetAnimation();
            return;
        }
    }

    /// <summary>
    /// 移動状態
    /// </summary>
    protected override void Move()
    {
        if (TransitionDead()) return;

        SetStopMovement(false);
        SetMoveAnimation();
        TransitionHit();

        if (GetIsAttached())
        {
            Debug.Log("Idleに遷移します");
            TransitionIdle();
        }
    }

    /// <summary>
    /// 攻撃状態
    /// </summary>
    protected override void Attack()
    {
        if (TransitionDead()) return;

        SetStopMovement(true);

        if (!GetIsAttached() && FinishedEventAnimation())
        {
            enemyState = EnemyState.Idle;
            ResetAnimation();
            return;
        }

        if (shortAttack.GetIsIdle())
        {
            ResetAnimation();
            SetAttackAnimation();
            shortAttack.SetStateAttack();
            return;
        }
        else if (shortAttack.GetIsRecast())
        {
            if (!FinishedEventAnimation()) return;

            enemyState = EnemyState.Idle;
            ResetAnimation();
            return;
        }
    }

    /// <summary>
    /// スキル状態
    /// </summary>
    protected void Skill()
    {
        if (TransitionDead()) return;

        SetStopMovement(true);

        if (!GetIsAttached() && FinishedEventAnimation())
        {
            enemyState = EnemyState.Idle;
            ResetAnimation();
            return;
        }

        if (isSkillShort ? shortSkill.GetIsIdle() : weaponSkill.GetIsIdle())
        {
            ResetAnimation();
            SetSkillAnimation();

            if (isSkillShort)
            {
                shortSkill.SetStateSkill();
            }
            else
            {
                weaponSkill.SetStateSkill();
            }

            return;
        }
        else if (isSkillShort ? shortSkill.GetIsRecast() : weaponSkill.GetIsRecast())
        {
            if (!FinishedEventAnimation()) return;

            enemyState = EnemyState.Idle;
            ResetAnimation();
            return;
        }
    }

    /// <summary>
    /// 被ダメージ状態
    /// </summary>
    protected override void Hit()
    {
        if (TransitionDead()) return;

        SetStopMovement(true);
        TransitionHit();

        if (FinishedEventAnimation())
        {
            TransitionIdle();
        }
    }

    /// <summary>
    /// 死亡状態
    /// </summary>
    protected override void Dead()
    {
        SetStopMovement(true);

        if (FinishedDeathAnimation())
        {
            DeathProcess();
            return;
        }
    }

    /// <summary>
    /// プレイヤーから攻撃を受けた際のヒットフラグ設定
    /// </summary>
    protected override void BossHit()
    {
        bossMove.SetIsHit(true);
    }

    /// <summary>
    /// スキルのアニメーションを設定
    /// </summary>
    protected void SetSkillAnimation()
    {
        if (onMidBossAnimation)
        {
            midBossAnimation.SetSkill();
        }
    }
}