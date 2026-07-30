using UnityEngine;

/// <summary>
/// ショートディスタンスアタック
/// 
/// 敵の近接攻撃処理を行うクラス
/// </summary>
public class ShortDistanceAttack : MonoBehaviour
{
    #region Config
    private int statAtk = 0; // 攻撃力
    private float statRng = 0f; // 範囲
    private float recastInterval = 1f; // 再攻撃インターバル
    #endregion

    #region State
    private GameObject attackToSpawn; // スポーンさせる攻撃のオブジェクト
    private float currentRecastInterval = 0f; // 現在の再攻撃インターバル
    private bool isAttacked = false; // 攻撃済みのフラグ
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 攻撃を行ったかどうか
        if (isAttacked)
            CompleteInterval();
    }

    /// @brief インターバルを消費する関数
    private void CompleteInterval()
    {
        // インターバルを消費しきっいない場合、recast時間を追加
        if(currentRecastInterval < recastInterval)
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
    
    /// @brief 攻撃オブジェクト（当たり判定）を生成する関数
    public void GenerateAttackObj()
    {
        // オブジェクトを生成
        GameObject spawnedAttack = Instantiate(attackToSpawn, transform.position, Quaternion.identity);
    }
}
