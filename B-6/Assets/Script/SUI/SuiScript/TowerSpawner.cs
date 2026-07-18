using UnityEngine;
using System.Collections;

public class TowerSpawner : MonoBehaviour
{
    // 壁素材のPrefab
    [SerializeField] private GameObject sozaiPrefab;

    // 出現範囲（最小半径・最大半径）
    [SerializeField] private float minSpawnRange = 3f;
    [SerializeField] private float maxSpawnRange = 10f;

    // 10秒ごとにドロップ
    [SerializeField] private float spawnInterval = 10f;

    // 20秒後に消滅
    [SerializeField] private float destroyTime = 20f;

    // 一度にドロップする数
    [SerializeField] private int dropCount = 3;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // 10秒待つ
            yield return new WaitForSeconds(spawnInterval);

            // 3個生成
            for (int i = 0; i < dropCount; i++)
            {
                // ランダムな角度
                float angle = Random.Range(0f, Mathf.PI * 2f);

                // ランダムな距離（面積が均等になるように）
                float minSqr = minSpawnRange * minSpawnRange;
                float maxSqr = maxSpawnRange * maxSpawnRange;
                float distance = Mathf.Sqrt(Random.Range(minSqr, maxSqr));

                // 出現位置
                Vector3 randomPos = transform.position + new Vector3(
                    Mathf.Cos(angle) * distance,
                    Mathf.Sin(angle) * distance,
                    0f
                );

                // 壁素材を生成
                GameObject sozai = Instantiate(sozaiPrefab, randomPos, Quaternion.identity);

                // 20秒後に削除
                Destroy(sozai, destroyTime);
            }
        }
    }
}