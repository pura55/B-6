using UnityEngine;
using System.Collections;

public class TowerSpawner : MonoBehaviour
{
    // 生成する素材のPrefab
    [SerializeField] private GameObject sozaiPrefab;

    // Towerを中心に素材を出現させる範囲（半径）
    [SerializeField] private float spawnRange = 5f;

    void Start()
    {
        // 素材を定期的に生成する処理を開始
        StartCoroutine(SpawnRoutine());
    }

    // 素材を一定時間ごとに生成する処理
    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // 5秒待つ
            yield return new WaitForSeconds(5f);

            // Towerを中心とした円の中からランダムな位置を取得
            // xが横方向、yが縦方向の位置になる
            // Random.insideUnitCircleは半径1の円の中からランダムな座標を返す
            Vector2 randomCircle = Random.insideUnitCircle * spawnRange;

            // Towerの位置にランダムな座標を足して、実際の出現位置を決定
            // 2DゲームなのでZ座標は0に固定
            Vector3 randomPos = transform.position + new Vector3(
                randomCircle.x,
                randomCircle.y,
                0f
            );

            // 指定した位置に素材を生成
            GameObject sozai = Instantiate(sozaiPrefab, randomPos, Quaternion.identity);

            // 30秒後に生成した素材を消す
            Destroy(sozai, 30f);
        }
    }
}