using UnityEngine;

/// <summary>
/// ベースエネミーアタック
/// 
/// 敵の攻撃処理の基底クラス
/// </summary>
public abstract class BaseEnemyAttack : MonoBehaviour
{
    protected enum EnemyAttackState
    {
        idle,      // 待機
        attack, // 攻撃
        recast,    // 攻撃待ち時間
    }

    #region Config
    protected int statAtk = 0; // 攻撃力
    protected float recastInterval = 1f; // 再攻撃インターバル
    [SerializeField] protected int enemyID; // 敵のID
    [SerializeField] protected int hitAnimationNumber; // 攻撃が当たるアニメーション番号
    #endregion

    #region State
    protected EnemyAttackState attackState = EnemyAttackState.idle;  // 敵の移動ステート
    protected float currentRecastInterval = 0f; // 現在の再攻撃インターバル
    protected bool isAttacked = false; // 攻撃済みのフラグ
    protected const string attackStatName = "ATK_DMG";  // ステータスの名前
    protected const string intervalStatName = "ATK_CT"; // ステータスの名前
    protected HitTriggerManager hitTrigger; // 当たり判定
    [SerializeField] protected EnemyProgressData enemyProgressData; // 敵のデータ
    #endregion

    /// @brief 攻撃を管理する関数 (子で上書き） 
    protected abstract void ManageAttacking();

    /// @brief 変数を初期化する関数（子で上書き）
    protected abstract void InitValue();

    /// @brief タイミングに合わせて攻撃処理を行う関数（子で上書き）
    protected abstract void Attacking();

    /// @brief リキャスト処理を行う関数（子で上書き）
    protected abstract void Recast();

    /// @brief インターバルを消費する関数
    protected void CompleteInterval()
    {
        // インターバルを消費しきっていない場合、recast時間を追加
        if (currentRecastInterval < recastInterval)
        {
            currentRecastInterval += Time.deltaTime;
        }
        else
        {
            // 消費しきった場合recast時間とフラグを初期化
            currentRecastInterval = 0f;
            isAttacked = false;
            return;
        }
    }


    /// @brief 攻撃状態に設定する関数
    public void SetStateAttack()
    {
        attackState = EnemyAttackState.attack;
    }

    /// @brief リキャスト状態に設定する関数
    public void SetStateRecast()
    {
        attackState = EnemyAttackState.recast;
    }

    /// @brief 攻撃フラグを設定する関数
    public void SetIsAttacked(bool attack)
    {
        isAttacked = attack;
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

    /// @brief 攻撃力を取得する関数
    public int GetStatAttack()
    {
        return statAtk;
    }

    /// @brief 待機状態かを確認する関数
    public bool GetIsIdle()
    {
        if (attackState == EnemyAttackState.idle) return true;

        return false;
    }

    /// @brief リキャスト状態かを確認する関数
    public bool GetIsRecast()
    {
        if (attackState == EnemyAttackState.recast) return true;

        return false;
    }
}
