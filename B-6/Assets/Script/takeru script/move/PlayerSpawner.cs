using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("プレイヤー")]
    [SerializeField] private GameObject playerPrefab;

    [Header("生成位置")]
    [SerializeField] private Transform spawnPoint;

    private GameObject spawnedPlayer;


    void Start()
    {
        SpawnPlayer();
    }


    private void SpawnPlayer()
    {
        // Prefab確認
        if (playerPrefab == null)
        {
            Debug.LogWarning(
                "Player Prefabが設定されていません"
            );

            return;
        }


        // =========================
        // 生成位置
        // =========================

        Vector3 spawnPosition;

        if (spawnPoint != null)
        {
            spawnPosition =
                spawnPoint.position;
        }
        else
        {
            // SpawnPoint未設定なら
            // PlayerSpawner自身の位置
            spawnPosition =
                transform.position;
        }


        // =========================
        // プレイヤー生成
        // =========================

        spawnedPlayer = Instantiate(
            playerPrefab,
            spawnPosition,
            Quaternion.identity
        );


        Debug.Log(
            "<color=green>プレイヤーを生成しました</color>"
        );
    }


    // 他のスクリプトから
    // 生成したPlayerを取得したい時用
    public GameObject GetPlayer()
    {
        return spawnedPlayer;
    }
}