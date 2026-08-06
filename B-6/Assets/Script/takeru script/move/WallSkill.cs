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
    [SerializeField] private float coolTime = 8f;
    private float nextWallTime = 0f;


    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            // CT中
            if (Time.time < nextWallTime)
            {
                float remain = nextWallTime - Time.time;
                Debug.Log($"壁生成クールタイム中 残り{remain:F1}秒");
                return;
            }


            // 素材チェック
            if (PartyManager.Instance.GetItemCount(wallMaterial) < requiredMaterial)
            {
                Debug.Log("壁素材が足りません");
                return;
            }


            // 素材消費
            PartyManager.Instance.RemoveItem(wallMaterial, requiredMaterial);


            // 次回使用可能時間設定
            nextWallTime = Time.time + coolTime;


            // マウス位置取得
            Vector3 mouseWorld =
                Camera.main.ScreenToWorldPoint(
                    Mouse.current.position.ReadValue());

            mouseWorld.z = 0f;


            // 方向
            Vector2 direction =
                (mouseWorld - transform.position).normalized;


            // 生成位置
            Vector3 spawnPos =
                transform.position + (Vector3)direction * distance;


            // 回転
            float angle =
                Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion rotation =
                Quaternion.Euler(0, 0, angle + 90f);


            // 壁生成
            GameObject wall =
                Instantiate(wallPrefab, spawnPos, rotation);


            Destroy(wall, lifeTime);
        }
    }
}