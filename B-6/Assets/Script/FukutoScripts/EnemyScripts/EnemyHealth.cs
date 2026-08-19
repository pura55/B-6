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

    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Rock"))
        {
            isHitRock = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //if (col.gameObject.CompareTag("PlayerAttack"))
        //{
        //    // 被ダメージ処理を実行
        //    ReciveDamage(col.gameObject);
        //
        //    if(!IsAlive())
        //        nomalEnemyManager.SetIsDead();
        //    else 
        //        nomalEnemyManager.SetTakeHit();
        //}
    }

    /// @brief 変数の初期化を行う関数
    protected void InitValue()
    {
        enemyHp = enemyProgressData.GetIntStat(enemyID, hpStatName);
        nomalEnemyManager = gameObject.GetComponent<NomalEnemyManager>();
    }

    /// @brief 被ダメージ処理を行う関数
    protected void ReciveDamage(GameObject attack)
    {
        enemyHp--;
    }

    /// @brief 生死を判定するフラグ
    protected bool IsAlive()
    {
        // hpが0だったらfalse
        if (enemyHp <= 0)
            return false;
        else
            return true;
    }

    /// @brief isHitRockを返す関数
    public bool GetHitRock()
    {
        return isHitRock;
    }
}
