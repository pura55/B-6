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

        PlayerHealth playerHealth =
                spawnedPlayer.GetComponentInChildren<PlayerHealth>();

        MoveScript moveScript =
            spawnedPlayer.GetComponent<MoveScript>();

        // 速度を設定
        if (moveScript != null)
        {
            moveScript.SetSpeed(playerProgressData.speed);
        }

        RespawnUI respawnUI =
              FindFirstObjectByType<RespawnUI>();

        if (respawnUI != null && playerHealth != null)
        {
            respawnUI.SetPlayer(playerHealth);
        }


        // =========================
        // 生成されたPlayerを確認
        // =========================

        SkillID1 skillID1 =
            spawnedPlayer.GetComponentInChildren<SkillID1>(true);

        SkillID2 skillID2 =
            spawnedPlayer.GetComponentInChildren<SkillID2>(true);

        SkillID3 skillID3 =
            spawnedPlayer.GetComponentInChildren<SkillID3>(true);

        SkillID4 skillID4 =
            spawnedPlayer.GetComponentInChildren<SkillID4>(true);


        Debug.Log(
            "<color=yellow>生成Player SkillID1：" +
            (skillID1 != null) +
            "</color>"
        );

        Debug.Log(
            "<color=yellow>生成Player SkillID2：" +
            (skillID2 != null) +
            "</color>"
        );

        Debug.Log(
            "<color=yellow>生成Player SkillID3：" +
            (skillID3 != null) +
            "</color>"
        );

        Debug.Log(
            "<color=yellow>生成Player SkillID4：" +
            (skillID4 != null) +
            "</color>"
        );


        // =========================
        // SkillManagerにPlayerを渡す
        // =========================

        SkillManager skillManager =
            FindFirstObjectByType<SkillManager>();

        if (skillManager != null)
        {
            skillManager.SetPlayer(spawnedPlayer);
        }
        else
        {
            Debug.LogWarning(
                "SkillManagerが見つかりません"
            );
        }


        Debug.Log(
            "<color=green>プレイヤーを生成しました</color>"
        );
    }


    public GameObject GetPlayer()
    {
        return spawnedPlayer;
    }
}