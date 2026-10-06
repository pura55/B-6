using UnityEngine;
using System.Collections;

/// <summary>
/// ノーマルエネミーマネージャー
/// 
/// 中ボスの管理を行うクラス
/// </summary>
public class MidBossManager : NomalEnemyManager
{
    #region Config
    [SerializeField] private int bossID; // ボスのID番号
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
        if (isSkillShort) shortSkill = GetComponent<ShortSkill>();
        else weaponSkill = GetComponent<WeaponSkill>();
        enemyState = EnemyState.Idle;
    }

    /// @brief 待機状態関数
    protected override void Idle()
    {
        if (TransitionDead())
        {
            return;
        }

        // 移動不可
        SetStopMovement(true);

        SetIdleAnimation();

        // 対象に近づいていない場合
        if (!GetIsAttached())
        {
            Debug.Log("Moveに遷移します");
            enemyState = EnemyState.Move;
            ResetAnimation();
            return;
        }
        else if (shortAttack.GetIsIdle()) // 待機時の時だけ攻撃へ遷移
        {
            enemyState = EnemyState.Attack;
            ResetAnimation();
            return;
        }
        else if ((isSkillShort ? shortSkill.GetIsIdle() : weaponSkill.GetIsIdle()))
        {
            enemyState = EnemyState.Skill;
            ResetAnimation();
            return;
        }

    }

    /// @brief 移動状態関数
    protected override void Move()
    {
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
            Debug.Log("Idleに遷移します");
            // 待機への遷移処理
            TransitionIdle();
        }
    }

    /// @brief 攻撃状態関数
    protected override void Attack()
    {
        if (TransitionDead())
        {
            return;
        }

        // 移動不可
        SetStopMovement(true);

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
        else if (shortAttack.GetIsRecast()) // リキャスト状態の場合
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
        if (TransitionDead())
        {
            return;
        }

        // 移動不可
        SetStopMovement(true);

        // 敵から離れている & イベントアニメーションが終了していたら
        if (!GetIsAttached() && FinishedEventAnimation())
        {
            // 一度待機に戻る
            enemyState = EnemyState.Idle;
            ResetAnimation();
            return;
        }
        // Idle状態だったら
        if ((isSkillShort ? shortSkill.GetIsIdle() : weaponSkill.GetIsIdle()))
        {
            ResetAnimation();
            SetSkillAnimation();
            if (isSkillShort) shortSkill.SetStateSkill();
            else weaponSkill.SetStateSkill();

            return;
        }
        else if ((isSkillShort ? shortSkill.GetIsRecast() : weaponSkill.GetIsRecast()))
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
        if (TransitionDead())
        {
            return;
        }

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
            // クリア判定
            ProcessClearCondition();
            // 死亡アニメーションが終了したら削除
            DeathProcess();
            return;
        }
    }

    /// @brief クリア判定を処理する関数
    private void ProcessClearCondition()
    {
        //// =========================
        //// Boss撃破でゲームクリア
        //// =========================
        if (bossID == 11)
        {
            StartCoroutine(WaitAndGameClear());
        }
    }

    /// @brief 2秒後にゲームクリアする関数
    private IEnumerator WaitAndGameClear()
    {
        // ボス撃破モーション後、2秒待つ
        yield return new WaitForSeconds(2f);

        GameClearManager gameClearManager =
            FindFirstObjectByType<GameClearManager>();

        if (gameClearManager != null)
        {
            gameClearManager.GameClear();
        }
        else
        {
            Debug.LogWarning(
                "GameClearManagerがシーンにありません"
            );
        }
    }

    /// @brief プレイヤーから攻撃を受けた際をヒットフラグ設定する関数
    protected override void BossHit()
    {
        bossMove.SetIsHit(true);
    }

    /// @brief スキルのアニメーションを設定する関数
    protected void SetSkillAnimation()
    {
        if (onMidBossAnimation) midBossAnimation.SetSkill();
    }
}