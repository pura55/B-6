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
        attacking, // 攻撃
        recast     // 攻撃待ち時間
    }

    #region Config
    protected int statAtk = 0; // 攻撃力
    protected float statRng = 0f; // 範囲
    protected float recastInterval = 1f; // 再攻撃インターバル
    [SerializeField] protected int enemyID; // 敵のID
    #endregion

    #region State
    protected EnemyAttackState attackState = EnemyAttackState.idle;  // 敵の移動ステート
    protected float currentRecastInterval = 0f; // 現在の再攻撃インターバル
    protected bool isAttacked = false; // 攻撃済みのフラグ
    protected const string attackStatName = "ATK_DMG";  // ステータスの名前
    protected const string intervalStatName = "ATK_CT"; // ステータスの名前
    protected GameObject hitBox; // 当たり判定
    [SerializeField] protected EnemyProgressData enemyProgressData; // 敵のデータ
    #endregion

    /// @brief 移動を管理する関数 (子で上書き） 
    protected abstract void ManageAttacking();

    /// @brief 変数を初期化する関数（子で上書き）
    protected abstract void InitValue();

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

    protected void ActiveHitBox()
    {
        hitBox.SetActive(true);
    }

    protected void InactiveHitBox()
    {
        hitBox.SetActive(false);
    }

    public int GetStatAttack()
    {
        return statAtk;
    }
}
