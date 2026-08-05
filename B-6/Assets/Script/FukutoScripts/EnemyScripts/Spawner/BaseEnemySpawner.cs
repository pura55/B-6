using UnityEngine;

/// <summary>
/// ベースエネミースポナー
/// 
/// 敵のスポナー基底クラス
/// </summary>
public abstract class BaseEnemySpawner : MonoBehaviour
{
    #region Config
    protected int currentSpawnIndex = 1;   // 現在のスポーンする回数
    protected int spawnCounter = 0;        // スポーンを数える変数
    protected const float spawnZ = 0f;     // z軸のスポーン座標
    protected float firstWaitTime = 3f;    // ゲームスタートの待機時間
    protected float spawnInterval = 10f;    // スポーンのインターバル
    #endregion

    #region State
    public Vector3 spawnRange = new Vector3(3f, 3f, 0f);   // スポーン範囲
    protected float currentWaitTime = 0f;                  // 現在の待ち時間 
    protected float currentSpawnInterval = 0f;             // 現在のスポーンインターバル
    protected bool isSpawn = false;                        // スポーンしたかどうかのフラグ（true: スポーンした、false:スポーンしてない）
    [SerializeField] protected GameObject enemyToSpawn;    // スポーンさせる対象オブジェクト
    [SerializeField] protected Transform tower;            // タワーのオブジェクト
    #endregion

    /// @brief 敵を生成する関数
    protected abstract void SpawnEnemy();

    /// @brief 座標を決めて生成する実体を返す関数
    protected GameObject GenerateInstance()
    {
        // ランダムな位置を計算
        float randomX = Random.Range(-spawnRange.x / 2, spawnRange.x / 2);
        float randomY = Random.Range(-spawnRange.y / 2, spawnRange.y / 2);

        // このスクリプトが付いているオブジェクトの位置を基準にする
        Vector3 spawnPosition = transform.position + new Vector3(randomX, randomY, spawnZ);

        return Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
    }

    /// @brief ゲームスタートの待機時間を計算してフラグを返す関数
    protected bool FirstWaitTimer()
    {
        if (currentWaitTime < firstWaitTime)
        {
            currentWaitTime = currentWaitTime + Time.deltaTime;
            return false;
        }

        return true;
    }

    /// @brief スポーンインターバルを消費する関数
    protected void CompleteSpawnInterval()
    {
        if (currentSpawnInterval < spawnInterval)
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

    /// @brief ターゲットの参照を渡す関数
    protected abstract void PassTargetReference(GameObject spawnedEnemy);

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnRange);
    }
}
