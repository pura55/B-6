using UnityEngine;

/// <summary>
/// ベースエネミーアタック
/// 
/// 敵の攻撃処理の基底クラス
/// </summary>
public abstract class BaseEnemyAttack : MonoBehaviour
{
    #region Config
    protected int statAtk = 0; // 攻撃力
    protected float statRng = 0f; // 範囲
    protected float recastInterval = 1f; // 再攻撃インターバル
    #endregion

    #region State
    protected GameObject hitBox; // 当たり判定
    protected float currentRecastInterval = 0f; // 現在の再攻撃インターバル
    protected bool isAttacked = false; // 攻撃済みのフラグ
    #endregion

    /// @brief インターバルを消費する関数
    protected void CompleteInterval()
    {
        // インターバルを消費しきっいない場合、recast時間を追加
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
}
