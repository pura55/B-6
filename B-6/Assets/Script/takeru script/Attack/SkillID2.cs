using UnityEngine;
using UnityEngine.InputSystem;

public class SkillID2 : MonoBehaviour
{
    [Header("キャラ2 スキル")]
    [SerializeField] private GameObject wavePrefab;

    [SerializeField] private float speed = 3f;
    [SerializeField] private float lifeTime = 10f;
    [SerializeField] private float waterRange = 8f;

    [Header("スキルストック")]
    [SerializeField] private SkillStock skillStock;

    public void UseSkill()
    {
        // ストック確認
        if (skillStock == null)
        {
            Debug.LogWarning("SkillStockが設定されていません");
            return;
        }

        if (!skillStock.UseStock())
        {
            return;
        }

        // Prefab確認
        if (wavePrefab == null)
        {
            Debug.LogWarning("ID2のWave Prefabが設定されていません");
            return;
        }

        if (Mouse.current == null || Camera.main == null)
            return;

        // マウス位置
        Vector3 mousePos =
            Mouse.current.position.ReadValue();

        mousePos.z =
            -Camera.main.transform.position.z;

        Vector3 worldPos =
            Camera.main.ScreenToWorldPoint(mousePos);

        worldPos.z = 0f;

        // 発射方向
        Vector2 direction =
            (worldPos - transform.position).normalized;

        // 回転
        float angle =
            Mathf.Atan2(direction.y, direction.x)
            * Mathf.Rad2Deg;

        // 波生成
        GameObject wave = Instantiate(
            wavePrefab,
            transform.position,
            Quaternion.Euler(0f, 0f, angle + 90f)
        );

        // 波のスクリプトに設定を渡す
        WaterWave waterWave =
            wave.GetComponent<WaterWave>();

        if (waterWave != null)
        {
            waterWave.Setup(
                transform,
                direction,
                speed,
                lifeTime,
                waterRange
            );
        }

        Debug.Log(
            "<color=blue>【ID2 スキル発動】</color>"
        );
    }
}