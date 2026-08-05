using UnityEngine;

/// <summary>
/// ベースエネミーマネージャー
/// 
/// 敵を管理処理を行う基底クラス
/// </summary>
public abstract class BaseEnemyManager : MonoBehaviour
{
    protected enum EnemyState
    {
        Idle = 1,      // 待機
        Move,      // 移動
        Attack,    // 攻撃
        Skill,     // スキル
        Hit,       // 被攻撃
        Dead,       // 死亡
    }

    #region Config
    [SerializeField] protected bool onNomalMove = false; // インスペクターにNomalMoveがあるかどうか
    [SerializeField] protected bool onAggressiveMove = false; // インスペクターにAggressiveMoveがあるかどうか
    [SerializeField] protected bool onNomalAnimation = false; // インスペクターにNomalAnimationがあるかどうか
    [SerializeField] protected bool onIncludeMovementAnimation = false; // インスペクターにIncludeMovementAnimationがあるかどうか
    #endregion

    #region State
    protected EnemyState enemyState = EnemyState.Idle; // 敵の状態
    protected bool isTakeHit = false; // ダメージを受けてかどうかのフラグ
    protected NomalMove nomalMove; // 普通の移動
    protected AggressiveMove aggressiveMove; // 攻撃的な移動
    protected NomalAnimation nomalAnimation; // 普通のアニメーション
    protected IncludeMovementAnimation movementAnimation; // 移動付きアニメーション
    protected EnemyHealth enemyHealth; // 体力管理スクリプト
    #endregion


    /// @brief 敵を管理する関数
    protected abstract void ManageEnemy();

    /// @brief 初期化関数
    protected abstract void InitValue();

    /// @brief 待機状態関数
    protected abstract void Idle();

    /// @brief 移動状態関数
    protected abstract void Move();

    /// @brief 攻撃状態関数
    protected abstract void Attack();

    /// @brief 被ダメージ状態関数
    protected abstract void Hit();

    /// @brief 死亡状態関数
    protected abstract void Dead();

    /// @brief 移動スクリプトを設定する関数
    protected void SetMovementScript()
    {
        // NomalMoveのコンポーネントの取得
        if (onNomalMove) nomalMove = GetComponent<NomalMove>();

        // AggressiveMoveのコンポーネントの取得
        else if (onAggressiveMove) aggressiveMove = GetComponent<AggressiveMove>();
    }

    /// @brief アニメーションスクリプトを設定する関数
    protected void SetAnimationScript()
    {
        // NomalAnimationeのコンポーネントの取得
        if (onNomalAnimation) nomalAnimation = GetComponent<NomalAnimation>();

        // IncludeMovementAnimationのコンポーネントの取得
        else if (onIncludeMovementAnimation) movementAnimation = GetComponent<IncludeMovementAnimation>();
    }

    /// @brief 移動の設定関数
    protected void SetStopMovement(bool flag)
    {
        if (onNomalMove) nomalMove.SetStopMovement(flag);

        else if(onAggressiveMove)aggressiveMove.SetStopMovement(flag);
    }

    /// @brief 接近フラグの取得関数
    protected bool GetIsAttached()
    {
        if (onNomalMove) return nomalMove.GetIsAttached();

        else if (onAggressiveMove) return aggressiveMove.GetIsAttached();

        else return true;
    }

    /// @brief 待機アニメーションを設定する関数
    protected void SetIdleAnimation()
    {
        if (onNomalAnimation) nomalAnimation.SetIdle ();

        else if (onIncludeMovementAnimation) movementAnimation.SetIdle();
    }

    /// @brief 移動アニメーションを設定する関数
    protected void SetMoveAnimation()
    {
        if (onNomalAnimation) nomalAnimation.SetMove();

        else if (onIncludeMovementAnimation) movementAnimation.SetMove();
    }

    /// @brief 攻撃アニメーションを設定する関数
    protected void SetAttackAnimation()
    {
        if (onNomalAnimation) nomalAnimation.SetAttack();

        else if (onIncludeMovementAnimation) movementAnimation.SetAttack();
    }

    /// @brief アニメーションをリセットする関数
    protected void ResetAnimation()
    {
        if (onNomalAnimation) nomalAnimation.ResetFrameAndIndex();

        else if (onIncludeMovementAnimation) movementAnimation.ResetFrameAndIndex();
    }

    protected bool FinishedEventAnimation()
    {
        if (onNomalAnimation)
            return nomalAnimation.GetFinishedEvent();

        else if (onIncludeMovementAnimation)
            return movementAnimation.GetFinishedEvent();

        return false;
    }
}
