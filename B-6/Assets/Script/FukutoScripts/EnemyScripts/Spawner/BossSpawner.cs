using UnityEngine;

public class BossSpawner : BaseEnemySpawner
{
    #region Config
    [SerializeField] private int bossID = 9;
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isSpawn = false;
        enemySpawnerManager = transform.parent.GetComponent<EnemySpawnerManager>();
        tower = enemySpawnerManager.GetTower().transform;
    }

    // Update is called once per frame
    void Update()
    {
        // スポーンフラグで敵生成を管理
        if (!isSpawn && enemySpawnerManager.GetSpawnBoss() == bossID)
        {
            // 敵生成
            SpawnEnemy();
            isSpawn = true;
            return;
        }
    }

    /// @brief 敵を生成する関数
    protected override void SpawnEnemy()
    {
        //現在の敵のスポーン数を超えたら処理を抜ける
        while (spawnCounter < currentSpawnIndex)
        {
            Debug.Log("敵のスポーン処理中");

            //オブジェクト生成
            GameObject spawnedEnemy = GenerateInstance(bossID - 8); // 雑魚敵分idを減少（リストで管理しているため）

            // ターゲットの参照を渡す
            PassTargetReference(spawnedEnemy, 1);

            //カウンターを増やす
            spawnCounter += 1;
        }

        // 初期化
        spawnCounter = 0;
    }

    /// @brief ターゲットの参照を渡す関数
    protected override void PassTargetReference(GameObject spawnedEnemy, int id)
    {
        // 敵のスクリプトの参照を取得
        BossMove enemyScript = spawnedEnemy.GetComponent<BossMove>();

        //敵にタワーのオブジェクトの参照を渡す
        if (enemyScript != null)
        {
            enemyScript.SetTargetTower(tower);
        }
    }
}
