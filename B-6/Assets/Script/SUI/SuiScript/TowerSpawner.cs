using UnityEngine;
using System.Collections;

public class TowerSpawner : MonoBehaviour
{
    // 生成する素材のPrefab
    [SerializeField] private GameObject sozaiPrefab;

    // 出現範囲（最小半径・最大半径）
    [SerializeField] private float minSpawnRange = 3f;
    [SerializeField] private float maxSpawnRange = 10f;

    // 生成間隔
    [SerializeField] private float spawnInterval = 5f;

    // 消えるまでの時間
    [SerializeField] private float destroyTime = 30f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // 一定時間待つ
            yield return new WaitForSeconds(spawnInterval);

            // ランダムな角度（0～360度）
            float angle = Random.Range(0f, Mathf.PI * 2f);

            // 面積が均等になるように距離を決める
            float minSqr = minSpawnRange * minSpawnRange;
            float maxSqr = maxSpawnRange * maxSpawnRange;
            float distance = Mathf.Sqrt(Random.Range(minSqr, maxSqr));

            // 出現位置を計算
            Vector3 randomPos = transform.position + new Vector3(
                Mathf.Cos(angle) * distance,
                Mathf.Sin(angle) * distance,
                0f
            );

            // 素材を生成
            GameObject sozai = Instantiate(sozaiPrefab, randomPos, Quaternion.identity);

            // 一定時間後に削除
            Destroy(sozai, destroyTime);
        }
    }
}