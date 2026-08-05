using UnityEngine;

/// <summary>
/// アグレッシブムービングスポナー
/// 
/// 攻撃的な敵を生成するクラス
/// </summary>
public class AggressiveMovingSpawner : BaseEnemySpawner
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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

            //オブジェクト生成
            GameObject spawnedEnemy = GenerateInstance();

            // ターゲットの参照を渡す
            PassTargetReference(spawnedEnemy);

            //カウンターを増やす
            spawnCounter += 1;
        }

        // 初期化
        spawnCounter = 0;
    }

    /// @brief ターゲットの参照を渡す関数
    protected override void PassTargetReference(GameObject spawnedEnemy)
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
