using UnityEngine;

/// <summary>
/// エネミーヘルス
/// 敵のHPを管理するクラス
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    #region Config
    private int enemyHp = 5;
    [SerializeField] private int enemyID = 0;
    private static int playerKillCount = 0;
    private bool playerKillCounted = false;
    private bool finalBossTimerStopped = false;
    #endregion

    #region State
    private bool isHitRock = false;
    private string hpStatName = "HP";
    [SerializeField] private EnemyProgressData enemyProgressData;

    [Header("ENEMY TYPE")]
    [SerializeField] private bool onNomalEnemy = true;
    [SerializeField] private bool onBoss = false;
    private NomalEnemyManager nomalEnemyManager;
    private MidBossManager midBossManager;

    private DamageTextManager damageTextManager;
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

    /// <summary>
    /// 変数の初期化
    /// </summary>
    private void InitValue()
    {
        enemyHp = enemyProgressData.GetIntStat(enemyID, hpStatName);

        if (onNomalEnemy)
        {
            nomalEnemyManager = GetComponent<NomalEnemyManager>();
        }
        else if (onBoss)
        {
            midBossManager = GetComponent<MidBossManager>();
        }

        damageTextManager = FindAnyObjectByType<DamageTextManager>();
    }

    /// <summary>
    /// 敵IDを取得
    /// </summary>
    public int GetEnemyID()
    {
        return enemyID;
    }

    /// <summary>
    /// 被ダメージ処理
    /// </summary>
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

    /// <summary>
    /// プレイヤーの攻撃による被ダメージ処理
    /// </summary>
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

    /// <summary>
    /// 通常の敵の被ダメージ処理
    /// </summary>
    private void NomalEnemyReceive(int dmg, bool fromPlayer)
    {
        if (nomalEnemyManager.GetIsDead()) return;

        enemyHp -= dmg;

        if (damageTextManager != null)
        {
            damageTextManager.ShowDamageText(transform.position, dmg, DamageTextManager.Target.ENEMY);
        }

        if (enemyHp < 0)
        {
            enemyHp = 0;
        }

        if (IsAlive())
        {
            nomalEnemyManager.SetTakeHit();
        }
        else
        {
            nomalEnemyManager.SetIsDead();

            // EnemyID 11のボスが死亡したらタイマーを停止・保存
            StopTimerIfFinalBoss();

            if (fromPlayer && !playerKillCounted)
            {
                playerKillCount++;
                playerKillCounted = true;
                Debug.Log("プレイヤー討伐数: " + playerKillCount);
            }
        }
    }

    /// <summary>
    /// ボスの被ダメージ処理
    /// </summary>
    private void BossReceive(int dmg, bool fromPlayer)
    {
        if (midBossManager.GetIsDead()) return;

        enemyHp -= dmg;

        if (damageTextManager != null)
        {
            damageTextManager.ShowDamageText(transform.position, dmg, DamageTextManager.Target.ENEMY);
        }

        if (enemyHp < 0)
        {
            enemyHp = 0;
        }

        if (IsAlive())
        {
            midBossManager.SetTakeHit();
        }
        else
        {
            midBossManager.SetIsDead();

            // EnemyID 11のボスが死亡したらタイマーを停止・保存
            StopTimerIfFinalBoss();

            if (fromPlayer && !playerKillCounted)
            {
                playerKillCount++;
                playerKillCounted = true;
            }
        }
    }

    /// <summary>
    /// EnemyID 11の死亡時にタイマーを停止して保存
    /// </summary>
    private void StopTimerIfFinalBoss()
    {
        if (enemyID != 11 || finalBossTimerStopped)
        {
            return;
        }

        finalBossTimerStopped = true;

        GameTimer gameTimer = FindFirstObjectByType<GameTimer>();

        if (gameTimer != null)
        {
            gameTimer.StopAndSaveTimer();
            Debug.Log(
                "EnemyID 11撃破。クリア時間：" +
                GameTimer.GetFinalElapsedTime() + "秒"
            );
        }
        else
        {
            Debug.LogError("GameTimerが見つかりません。時間を保存できませんでした。");
        }
    }

    /// <summary>
    /// 生死を判定
    /// </summary>
    private bool IsAlive()
    {
        return enemyHp > 0;
    }

    /// <summary>
    /// プレイヤーが倒した敵の数を取得
    /// </summary>
    public static int GetPlayerKillCount()
    {
        return playerKillCount;
    }

    /// <summary>
    /// プレイヤーが倒した敵の数をリセット
    /// </summary>
    public static void ResetPlayerKillCount()
    {
        playerKillCount = 0;
    }

    /// <summary>
    /// 岩に当たったかどうかを設定
    /// </summary>
    public void SetHitRock()
    {
        isHitRock = true;
        Debug.Log("敵に当たりました！");
    }

    /// <summary>
    /// 岩に当たったかどうかを取得
    /// </summary>
    public bool GetHitRock()
    {
        return isHitRock;
    }

    /// <summary>
    /// 敵が死亡しているかどうかを返す
    /// </summary>
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

    /// <summary>
    /// ダメージテキストマネージャーを設定
    /// </summary>
    public void SetDamageText(DamageTextManager manager)
    {
        damageTextManager = manager;
    }
}