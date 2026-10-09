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
    private WeaponSkill weaponSkill; // 武器スキル
    private ShortSkill shortSkill;
    private int actionCount; // 行動をカウント
    private const int maxActionCount = 5; // 行動値の最大カウント
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

        // 近づいている場合　or 行動値が達していた場合
        if (!GetIsAttached() || maxActionCount <= actionCount)
        {
            Debug.Log("Moveに遷移します");
            enemyState = EnemyState.Move;
            ResetAnimation();
            actionCount = 0; // 行動値をリセット
            return;
        }
        else if (shortAttack.GetIsIdle())
        {
            Debug.Log("Attackに遷移します");
            enemyState = EnemyState.Attack;
            ResetAnimation();
            actionCount++;
            return;
        }
        else if (isSkillShort ? shortSkill.GetIsIdle() : weaponSkill.GetIsIdle())
        {
            Debug.Log("Skillに遷移します");
            enemyState = EnemyState.Skill;
            ResetAnimation();
            actionCount++;
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

        // 対象に近づいていない＋アニメーションが終了したとき
        if (!GetIsAttached() && FinishedEventAnimation())
        {
            // 待機へ遷移
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