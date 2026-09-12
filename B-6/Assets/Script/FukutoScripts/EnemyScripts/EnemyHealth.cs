using UnityEngine;

/// <summary>
/// エネミーヘルス
/// 
/// 敵のHPを管理するクラス
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    #region Config
    protected int enemyHp =5; // 敵のHP
    [SerializeField] protected int enemyID = 0;
    #endregion

    #region State
    protected bool isHitRock = false;
    protected string hpStatName = "HP"; // ステータスの名前
    [SerializeField] protected EnemyProgressData enemyProgressData; // 敵のデータ
    protected NomalEnemyManager nomalEnemyManager; // エネミーマネージャー
    #endregion

    void Start()
    {
        InitValue();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Rock"))
        {
            isHitRock = true;
        }
    }

    /// @brief 変数の初期化を行う関数
    protected void InitValue()
    {
        enemyHp = enemyProgressData.GetIntStat(enemyID, hpStatName);
        nomalEnemyManager = gameObject.GetComponent<NomalEnemyManager>();
    }

    /// @brief 被ダメージ処理を行う関数
    public void ReceiveDamage(int dmg)
    {
        // マネージャーの死亡フラグがtrueの時これ以降の処理を行わない
        if (nomalEnemyManager.GetIsDead()) return;

        // ダメージ分体力を減少指せる
        enemyHp -= dmg;
        Debug.Log("敵のHP : " + enemyHp);

        // 0未満の場合0に設定
        if (enemyHp < 0)
        {
            enemyHp = 0;
        }

        // 生きている場合
        if (IsAlive())
        {
            nomalEnemyManager.SetTakeHit();
        } 
        else // 死んでいる場合
        {
            // 死亡フラグをtrue
            nomalEnemyManager.SetIsDead();
            nomalEnemyManager.SetTakeHit();
        }
            
    }

    /// @brief 生死を判定するフラグ
    protected bool IsAlive()
    {
        // hpが0より大きい場合
        if (enemyHp > 0) return true;
        else return false;
            
    }

    /// @brief isHitRockを返す関数
    public bool GetHitRock()
    {
        return isHitRock;
    }
}
