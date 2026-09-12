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

    public int RequiredMaterial => requiredMaterial;

    public bool IsCoolTime()
    {
        return Time.time < nextWallTime;
    }

    public float GetCoolTimeRate()
    {
        if (coolTime <= 0f)
            return 0f;

        float remain = nextWallTime - Time.time;

        return Mathf.Clamp01(remain / coolTime);
    }

    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (Time.time < nextWallTime)
            {
                float remain = nextWallTime - Time.time;
                Debug.Log($"壁生成クールタイム中 残り{remain:F1}秒");
                return;
            }

            if (PartyManager.Instance.GetItemCount(wallMaterial) < requiredMaterial)
            {
                Debug.Log("壁素材が足りません");
                return;
            }

            PartyManager.Instance.RemoveItem(
                wallMaterial,
                requiredMaterial
            );

            nextWallTime = Time.time + coolTime;

            Vector3 mouseWorld =
                Camera.main.ScreenToWorldPoint(
                    Mouse.current.position.ReadValue()
                );

            mouseWorld.z = 0f;

            Vector2 direction =
                (mouseWorld - transform.position).normalized;

            Vector3 spawnPos =
                transform.position +
                (Vector3)direction * distance;

            float angle =
                Mathf.Atan2(direction.y, direction.x)
                * Mathf.Rad2Deg;

            Quaternion rotation =
                Quaternion.Euler(
                    0,
                    0,
                    angle + 90f
                );

            GameObject wall =
                Instantiate(
                    wallPrefab,
                    spawnPos,
                    rotation
                );

            Destroy(wall, lifeTime);
        }
    }
}