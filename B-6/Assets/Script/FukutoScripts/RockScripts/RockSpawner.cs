using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    #region Config
    private int currentSpawnIndex = 1;   // 現在のスポーンする回数
    private int spawnCounter = 0;        // スポーンを数える変数
    private const float spawnZ = 0f;     // z軸のスポーン座標
    #endregion

    #region State
    [SerializeField] private GameObject rockToSpawn;     // スポーンさせる対象オブジェクト
    [SerializeField] private Transform tower;            // タワーのオブジェクト
    [SerializeField] private GameObject rockManager;     // 落石を管理するオブジェクト
    public Vector3 spawnRange = new Vector3(3f, 3f, 0f); // スポーン範囲
    private bool isSpawn = false;                        // スポーンしたかどうかのフラグ（true: スポーンした、false:スポーンしてない）
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RockManager managementScript = rockManager.GetComponent<RockManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawn)
        {
            // 敵生成
            RockSpawn();
            isSpawn = true;
            return;
        }
    }

    // 敵を生成する関数
    private void RockSpawn()
    {
        //現在の落石のスポーン数を超えたら処理を抜ける
        while (spawnCounter < currentSpawnIndex)
        {
            Debug.Log("落石のスポーン処理中");

            // ランダムな位置を計算
            float randomX = Random.Range(-spawnRange.x / 2, spawnRange.x / 2);
            float randomY = Random.Range(-spawnRange.y / 2, spawnRange.y / 2);

            // このスクリプトが付いているオブジェクトの位置を基準にする
            Vector3 spawnPosition = transform.position + new Vector3(randomX, randomY, spawnZ);

            // オブジェクト生成
            GameObject spawnedRock = Instantiate(rockToSpawn, spawnPosition, Quaternion.identity);

            // 落石のスクリプトの参照を取得
            RockRoll rockScript = spawnedRock.GetComponent<RockRoll>();

            // 落石にタワーのオブジェクトの参照を渡す
            if (rockScript != null)
            {
                rockScript.SetTargetTower(tower);
            }

            //カウンターを増やす
            spawnCounter += 1;
        }

        // 初期化
        spawnCounter = 0;
    }

    // 開発画面（Scene）に生成範囲を視覚的に表示する機能
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnRange);
    }
}
