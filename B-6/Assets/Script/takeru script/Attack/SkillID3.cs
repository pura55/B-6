using UnityEngine;
using UnityEngine.InputSystem;

public class SkillID3 : MonoBehaviour
{
    [Header("キャラ3 風スキル")]
    [SerializeField] private GameObject windPrefab;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifeTime = 3f;

    [Header("スキルストック")]
    [SerializeField] private SkillStock skillStock;

    public bool UseSkill()
    {
        if (skillStock == null)
        {
            Debug.LogWarning("SkillStockが設定されていません");
            return false;
        }

        if (windPrefab == null)
        {
            Debug.LogWarning("ID3のWind Prefabが設定されていません");
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

        GameObject wind = Instantiate(
            windPrefab,
            transform.position,
            Quaternion.Euler(0f, 0f, angle - 90f)
        );

        WindWave windWave =
            wind.GetComponent<WindWave>();

        if (windWave != null)
        {
            windWave.Setup(
                direction,
                speed,
                lifeTime
            );
        }

        Debug.Log(
            "<color=green>【ID3 風スキル発動】</color>"
        );

        return true;
    }
}