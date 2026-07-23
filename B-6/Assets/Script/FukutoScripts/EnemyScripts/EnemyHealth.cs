using UnityEngine;

/// <summary>
/// エネミーヘルス
/// 
/// 敵のHPを管理するクラス
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    #region Config
    private int enemyHp =5; // 敵のHP
    #endregion

    #region State

    #endregion 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Rock"))
        {
            // 死亡処理を実行
            DeathProcessing();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PlayerAttack"))
        {
            // 被ダメージ処理を実行
            ReciveDamage(col.gameObject);
        }
    }

    /// @brief 被ダメージ処理を行う関数
    private void ReciveDamage(GameObject attack)
    {
        enemyHp--;
    }

    /// @brief 死亡処理を行う関数
    private void DeathProcessing()
    {
        // 経験値を落とすスクリプトの参照を取得
        DropExp dropExp = gameObject.GetComponent <DropExp>();

        // スクリプトがnullではない場合実行
        if (dropExp != null)
        {
            // 経験値ドロップ処理を実行
            dropExp.EnemyDropExp();
        }

        // エネミーを削除
        Destroy(gameObject);
    }
}
