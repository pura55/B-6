using UnityEngine;

/// <summary>
/// エネミーヘルス
/// 
/// 敵のHPを管理するクラス
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    #region Config
    private int enemyHp = 5; // 敵のHP
    [SerializeField] private int enemyID = 0;
    private static int playerKillCount = 0; // プレイヤーが倒した敵の数
    private bool playerKillCounted = false; // 討伐数を重複して数えないためのフラグ
    #endregion

    #region State
    private bool isHitRock = false;
    private string hpStatName = "HP"; // ステータスの名前
    [SerializeField] private EnemyProgressData enemyProgressData; // 敵のデータ

    [Header("ENEMY TYPE")]
    [SerializeField] private bool onNomalEnemy = true; // 通常の敵かどうかのフラグ
    [SerializeField] private bool onBoss = false; // ボスかどうかのフラグ
    private NomalEnemyManager nomalEnemyManager; // エネミーマネージャー
    private MidBossManager midBossManager; // 中ボス（ボス）のマネージャー

    private DamageTextManager damageTextManager; // ダメージテキストマネージャー
    #endregion

    void Start()
    {
        InitValue();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //if (col.gameObject.CompareTag("Rock"))
        //{
        //    isHitRock = true;
        //}
    }

    /// @brief 変数の初期化を行う関数
    private void InitValue()
    {
        enemyHp = enemyProgressData.GetIntStat(enemyID, hpStatName);

        if (onNomalEnemy)
        {
            nomalEnemyManager = gameObject.GetComponent<NomalEnemyManager>();
        }
        else if (onBoss)
        {
            midBossManager = gameObject.GetComponent<MidBossManager>();
        }

        damageTextManager = FindAnyObjectByType<DamageTextManager>();
    }

    /// @brief 被ダメージ処理を行う関数
    public void ReceiveDamage(int dmg)
    {
        if (onNomalEnemy)
        {
            NomalEnemyReceive(dmg, false);
        }
        else if (onBoss)
        {
            BossReceive(dmg, false);
        }
    }

    /// @brief プレイヤーの攻撃による被ダメージ処理を行う関数
    public void ReceivePlayerDamage(int dmg)
    {
        if (onNomalEnemy)
        {
            NomalEnemyReceive(dmg, true);
        }
        else if (onBoss)
        {
            BossReceive(dmg, true);
        }
    }

    /// @brief 普通の敵の被ダメージ処理を行う関数
    private void NomalEnemyReceive(int dmg, bool fromPlayer)
    {
        // マネージャーの死亡フラグがtrueの時これ以降の処理を行わない
        if (nomalEnemyManager.GetIsDead()) return;

        // ダメージ分体力を減少させる
        enemyHp -= dmg;
        Debug.Log("敵のHP : " + enemyHp);

        // ダメージ表示
        damageTextManager.ShowDamageText(transform.position, dmg);

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

            // プレイヤーが倒した場合のみ討伐数を加算
            if (fromPlayer && !playerKillCounted)
            {
                playerKillCount++;
                playerKillCounted = true;
                Debug.Log("プレイヤー討伐数: " + playerKillCount);
            }
        }
    }

    /// @brief ボスの被ダメージ処理を行う関数
    private void BossReceive(int dmg, bool fromPlayer)
    {
        // マネージャーの死亡フラグがtrueの時これ以降の処理を行わない
        if (midBossManager.GetIsDead()) return;

        // ダメージ分体力を減少させる
        enemyHp -= dmg;
        Debug.Log("敵のHP : " + enemyHp);

        // ダメージ表示
        damageTextManager.ShowDamageText(transform.position, dmg);

        // 0未満の場合0に設定
        if (enemyHp < 0)
        {
            enemyHp = 0;
        }

        // 生きている場合
        if (IsAlive())
        {
            midBossManager.SetTakeHit();
        }
        else // 死んでいる場合
        {
            // 死亡フラグをtrue
            midBossManager.SetIsDead();

            // プレイヤーが倒した場合のみ討伐数を加算
            if (fromPlayer && !playerKillCounted)
            {
                playerKillCount++;
                playerKillCounted = true;
            }
        }
    }

    /// @brief 生死を判定するフラグ
    private bool IsAlive()
    {
        // hpが0より大きい場合
        if (enemyHp > 0) return true;
        else return false;
    }

    /// @brief プレイヤーが倒した敵の数を返す関数
    public static int GetPlayerKillCount()
    {
        return playerKillCount;
    }

    /// @brief プレイヤーが倒した敵の数をリセットする関数
    public static void ResetPlayerKillCount()
    {
        playerKillCount = 0;
    }

    /// @brief 岩に当たったかどうかのフラグを設定する関数
    public void SetHitRock()
    {
        isHitRock = true;
        Debug.Log("敵に当たりました！");
    }

    /// @brief 岩に当たったかどうかのフラグを返す関数
    public bool GetHitRock()
    {
        return isHitRock;
    }

    /// 敵が死亡しているかどうかを返す
    public bool IsDead()
    {
        if (onNomalEnemy)
        {
            return nomalEnemyManager != null &&
                   nomalEnemyManager.GetIsDead();
        }

        if (onBoss)
        {
            return midBossManager != null &&
                   midBossManager.GetIsDead();
        }

        return false;
    }

    /// @biref　ダメージテキストマネージャーを設定する関数
    public void SetDamageText(DamageTextManager manager)
    {
        damageTextManager = manager;
    }
}