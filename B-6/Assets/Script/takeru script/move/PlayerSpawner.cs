using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("プレイヤー")]
    [SerializeField] private GameObject playerPrefab;

    [Header("生成位置")]
    [SerializeField] private Transform spawnPoint;

    [Header("キャラクターID")]
    [SerializeField] private SelectCharacterID selectCharacterID;

    [Header("プレイヤーデータ")]
    [SerializeField] private PlayerProgressData playerProgressData;

    private GameObject spawnedPlayer;


    void Start()
    {
        // =========================
        // 選択したキャラIDを反映
        // =========================

        if (selectCharacterID != null &&
            playerProgressData != null)
        {
            playerProgressData.id =
                selectCharacterID.GetSelectID();

            Debug.Log(
                $"<color=cyan>選択キャラID：{playerProgressData.id}</color>"
            );
        }

        SpawnPlayer();
    }


    private void SpawnPlayer()
    {
        if (playerPrefab == null)
        {
            Debug.LogWarning(
                "Player Prefabが設定されていません"
            );

            return;
        }


        Vector3 spawnPosition;

        if (spawnPoint != null)
        {
            spawnPosition =
                spawnPoint.position;
        }
        else
        {
            spawnPosition =
                transform.position;
        }


        spawnedPlayer = Instantiate(
            playerPrefab,
            spawnPosition,
            Quaternion.identity
        );


        Debug.Log(
            "<color=green>プレイヤーを生成しました</color>"
        );
    }


    public GameObject GetPlayer()
    {
        return spawnedPlayer;
    }
}