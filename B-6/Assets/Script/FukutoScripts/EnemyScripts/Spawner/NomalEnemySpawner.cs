using UnityEngine;

public class NomalEnemySpawner : BaseEnemySpawner
{
    #region Config
    [Header("POSITON")]
    [SerializeField] private Vector3 activeSpawnPosition = new Vector3(0f, 0f, 0f); // 攻撃的スポーン範囲
    [SerializeField] private Vector3 nomalSpawnPosition = new Vector3(0f, 0f, 0f); // 普通スポーン範囲
    #endregion
    void Start()
    {
        nomalSpawnPosition = transform.position;
        enemySpawnerManager = transform.parent.GetComponent<EnemySpawnerManager>();
        tower = enemySpawnerManager.GetTower().transform;
    }

    // Update is called once per frame
    void Update()
    {
        // ゲームスタートの待機時間が過ぎたらそれ以降の処理を開始
        if (!FirstWaitTimer())
        {
            return;
        }

        // スポーンフラグで敵生成を管理
        if (!isSpawn)
        {
            // 敵生成
            SpawnEnemy();
            isSpawn = true;
            return;
        }
        else
        {
            // スポーンインターバルを加算
            CompleteSpawnInterval();
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

            // スポーンする敵IDを取得
            int[] spawnID = enemySpawnerManager.GetSpawnEnemies();

            // ランダムに敵を選別
            int random = Random.Range(0, spawnID.Length);

            if(spawnID[random] < 5)
            {
                transform.position = nomalSpawnPosition;
            }
            else
            {
                transform.position = activeSpawnPosition;
            }

            //オブジェクト生成
            GameObject spawnedEnemy = GenerateInstance(spawnID[random]);

            // ターゲットの参照を渡す
            PassTargetReference(spawnedEnemy, spawnID[random]);
             
            //カウンターを増やす
            spawnCounter += 1;
        }

        // 初期化
        spawnCounter = 0;
    }

    /// @brief ターゲットの参照を渡す関数
    protected override void PassTargetReference(GameObject spawnedEnemy, int id)
    {
        // IDによってスクリプトの参照方法を変更
        if(id < 5)
        {
            // 敵のスクリプトの参照を取得
            NomalMove enemyScript = spawnedEnemy.GetComponent<NomalMove>();

            //敵にタワーのオブジェクトの参照を渡す
            if (enemyScript != null)
            {
                enemyScript.SetTargetTower(tower);
            }
        }
        else
        {
            // 敵のスクリプトの参照を取得
            AggressiveMove enemyScript = spawnedEnemy.GetComponent<AggressiveMove>();

            //敵にタワーのオブジェクトの参照を渡す
            if (enemyScript != null)
            {
                enemyScript.SetTargetTower(tower);
            }
        }
        
    }
}
