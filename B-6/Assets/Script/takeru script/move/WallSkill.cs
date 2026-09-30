using UnityEngine;
using UnityEngine.InputSystem;

public class WallSkill : MonoBehaviour
{
    [Header("壁の設定")]
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private float distance = 1.5f;
    [SerializeField] private float lifeTime = 20f;

    [Header("壁素材")]
    [SerializeField] private ItemData wallMaterial;
    [SerializeField] private int requiredMaterial = 10;

    [Header("クールタイム")]
    [SerializeField] public float coolTime = 8f;

    private float nextWallTime = 0f;

    // プレイヤーHP
    private PlayerHealth playerHealth;


    public int RequiredMaterial => requiredMaterial;


    void Start()
    {
        // PlayerHealth取得
        playerHealth = GetComponent<PlayerHealth>();
    }


    public bool IsCoolTime()
    {
        return Time.time < nextWallTime;
    }


    public float GetCoolTimeRate()
    {
        if (coolTime <= 0f)
            return 0f;

        float remain =
            nextWallTime - Time.time;

        return Mathf.Clamp01(
            remain / coolTime
        );
    }


    void Update()
    {
        // =========================
        // 死亡中は壁生成不可
        // =========================
        if (playerHealth != null &&
            playerHealth.IsDead)
        {
            return;
        }


        if (Keyboard.current == null)
            return;


        if (!Keyboard.current.eKey.wasPressedThisFrame)
            return;


        // =========================
        // クールタイム確認
        // =========================
        if (Time.time < nextWallTime)
        {
            float remain =
                nextWallTime - Time.time;

            Debug.Log(
                $"壁生成クールタイム中 残り{remain:F1}秒"
            );

            return;
        }


        // =========================
        // PartyManager確認
        // =========================
        if (PartyManager.Instance == null)
        {
            Debug.LogWarning(
                "PartyManagerがありません"
            );

            return;
        }


        // =========================
        // 素材確認
        // =========================
        if (PartyManager.Instance.GetItemCount(wallMaterial)
            < requiredMaterial)
        {
            Debug.Log(
                "壁素材が足りません"
            );

            return;
        }


        // =========================
        // 素材消費
        // =========================
        PartyManager.Instance.RemoveItem(
            wallMaterial,
            requiredMaterial
        );


        // =========================
        // クールタイム開始
        // =========================
        nextWallTime =
            Time.time + coolTime;


        // =========================
        // マウス位置
        // =========================
        Vector3 mouseWorld =
            Camera.main.ScreenToWorldPoint(
                Mouse.current.position.ReadValue()
            );

        mouseWorld.z = 0f;


        // =========================
        // プレイヤー → マウス方向
        // =========================
        Vector2 direction =
            (mouseWorld - transform.position)
            .normalized;


        // =========================
        // 壁生成位置
        // =========================
        Vector3 spawnPos =
            transform.position +
            (Vector3)direction * distance;


        // =========================
        // 壁の向き
        // =========================
        float angle =
            Mathf.Atan2(
                direction.y,
                direction.x
            )
            * Mathf.Rad2Deg;


        Quaternion rotation =
            Quaternion.Euler(
                0f,
                0f,
                angle + 90f
            );


        // =========================
        // 壁生成
        // =========================
        GameObject wall =
            Instantiate(
                wallPrefab,
                spawnPos,
                rotation
            );


        Destroy(
            wall,
            lifeTime
        );
    }
}