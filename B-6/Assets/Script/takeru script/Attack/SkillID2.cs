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

    public bool UseSkill()
    {
        if (skillStock == null)
        {
            Debug.LogWarning("SkillStockが設定されていません");
            return false;
        }

        if (wavePrefab == null)
        {
            Debug.LogWarning("ID2のWave Prefabが設定されていません");
            return false;
        }

        if (Mouse.current == null || Camera.main == null)
            return false;

        if (!skillStock.UseStock())
            return false;

        Vector3 mousePos =
            Mouse.current.position.ReadValue();

        mousePos.z =
            -Camera.main.transform.position.z;

        Vector3 worldPos =
            Camera.main.ScreenToWorldPoint(mousePos);

        worldPos.z = 0f;

        Vector2 direction =
            (worldPos - transform.position).normalized;

        float angle =
            Mathf.Atan2(direction.y, direction.x)
            * Mathf.Rad2Deg;

        GameObject wave = Instantiate(
            wavePrefab,
            transform.position,
            Quaternion.Euler(0f, 0f, angle + 90f)
        );

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

        return true;
    }
}