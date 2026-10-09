using UnityEngine;

/// <summary>
/// ベースエネミースキル
/// 
/// スキルのベースクラス
/// </summary>
public abstract class BaseEnemySkill : MonoBehaviour
{
    protected enum EnemySkillState
    {
        idle,      // 待機
        skill, // 攻撃
        recast,    // 攻撃待ち時間
    }

    #region Config
    protected int statSkill = 0; // 攻撃力
    protected float recastInterval = 1f; // 再攻撃インターバル
    [SerializeField] protected int enemyID; // 敵のID
    [SerializeField] protected int hitAnimationNumber; // スキルが当たるアニメーション番号
    #endregion

    #region State
    protected EnemySkillState skillState = EnemySkillState.idle;  // 敵の移動ステート
    protected float currentRecastInterval = 0f; // 現在の再攻撃インターバル
    protected bool isAttacked = false; // 攻撃済みのフラグ
    protected const string skillStatName = "SKILL_DMG";  // ステータスの名前
    protected const string intervalStatName = "SKILL_CT"; // ステータスの名前
    [SerializeField] protected EnemyProgressData enemyProgressData; // 敵のデータ
    protected MidBossAnimation midBossAnimation; // 中ボスのアニメーション
    #endregion

    /// @brief 攻撃を管理する関数 (子で上書き） 
    protected abstract void ManageAttacking();

    /// @brief 変数を初期化する関数（子で上書き）
    protected abstract void InitValue();

    /// @brief タイミングに合わせて攻撃処理を行う関数（子で上書き）
    protected abstract void UsingSkill();

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
    public void SetStateSkill()
    {
        skillState = EnemySkillState.skill;
    }

    /// @brief リキャスト状態に設定する関数
    public void SetStateRecast()
    {
        skillState = EnemySkillState.recast;
    }

    /// @brief 攻撃フラグを取得する関数
    public bool GetIsAttacked()
    {
        return isAttacked;
    }

    /// @brief スキルの攻撃力を取得する関数
    public int GetStatSkill()
    {
        return statSkill;
    }

    /// @brief 待機状態かを確認する関数
    public bool GetIsIdle()
    {
        if (skillState == EnemySkillState.idle) return true;

        return false;
    }

    /// @brief リキャスト状態かを確認する関数
    public bool GetIsRecast()
    {
        if (skillState == EnemySkillState.recast) return true;

        return false;
    }
}
