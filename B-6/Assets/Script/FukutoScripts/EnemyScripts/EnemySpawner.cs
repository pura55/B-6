using UnityEngine;

/// <summary>
/// エネミースポナー
/// 
/// 敵の生成処理を行うクラス
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    #region Config
    private int currentSpawnIndex = 1;   // 現在のスポーンする回数
    private int spawnCounter = 0;        // スポーンを数える変数
    private const float spawnZ = 0f;     // z軸のスポーン座標
    private float firstWaitTime = 3f;    // ゲームスタートの待機時間
    private float spawnInterval = 10f;    // スポーンのインターバル
    #endregion

    #region State
    [SerializeField] private GameObject enemyToSpawn;    // スポーンさせる対象オブジェクト
    [SerializeField] private Transform tower;            // タワーのオブジェクト
    public Vector3 spawnRange = new Vector3(3f, 3f, 0f); // スポーン範囲
    private float currentWaitTime = 0f;                  // 現在の待ち時間 
    private float currentSpawnInterval = 0f;             // 現在のスポーンインターバル
    private bool isSpawn = false;                        // スポーンしたかどうかのフラグ（true: スポーンした、false:スポーンしてない）
    #endregion
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
        if(!isSpawn)
        {
            // 敵生成
            EnemySpawn();
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
    private void EnemySpawn()
    {
        //現在の敵のスポーン数を超えたら処理を抜ける
        while (spawnCounter < currentSpawnIndex)
        {
            Debug.Log("敵のスポーン処理中");

            // ランダムな位置を計算
            float randomX = Random.Range(-spawnRange.x / 2, spawnRange.x / 2);
            float randomY = Random.Range(-spawnRange.y / 2, spawnRange.y / 2);

            // このスクリプトが付いているオブジェクトの位置を基準にする
            Vector3 spawnPosition = transform.position + new Vector3(randomX, randomY, spawnZ);

            //オブジェクト生成
            GameObject spawnedEnemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);

            // 敵のスクリプトの参照を取得
            EnemyMove enemyScript = spawnedEnemy.GetComponent<EnemyMove>();

            //敵にタワーのオブジェクトの参照を渡す
            if (enemyScript != null)
            {
                enemyScript.SetTargetTower(tower);
            }

            //カウンターを増やす
            spawnCounter += 1;
        }

        // 初期化
        spawnCounter = 0;
    }

    /// @brief ゲームスタートの待機時間を計算してフラグを返す関数
    private bool FirstWaitTimer()
    {
        if(currentWaitTime < firstWaitTime)
        {
            currentWaitTime = currentWaitTime + Time.deltaTime;
            return false;
        }

        return true;
    }

    /// @brief スポーンインターバルを消費する関数
    private void CompleteSpawnInterval()
    {
        if(currentSpawnInterval < spawnInterval)
        {
            currentSpawnInterval = currentSpawnInterval + Time.deltaTime;
            return;
        }
        else
        {
            currentSpawnInterval = 0f;
            isSpawn = false;
        }
    }

    // 開発画面（Scene）に生成範囲を視覚的に表示する機能
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnRange);
    }
}
