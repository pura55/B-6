using UnityEngine;
using UnityEngine.InputSystem;

public class WallSkill : MonoBehaviour
{
    [Header("壁の設定")]
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private float distance = 1.5f;
    [SerializeField] private float lifeTime = 20f;

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            // マウス位置をワールド座標に変換
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mouseWorld.z = 0f;

            // プレイヤーからマウスへの方向
            Vector2 direction = (mouseWorld - transform.position).normalized;

            // 壁を出す位置
            Vector3 spawnPos = transform.position + (Vector3)direction * distance;

            // 壁をカーソル方向に対して横向きにする
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle + 90f);

            // 壁を生成
            GameObject wall = Instantiate(wallPrefab, spawnPos, rotation);

            // 一定時間後に削除
            Destroy(wall, lifeTime);
        }
    }
}