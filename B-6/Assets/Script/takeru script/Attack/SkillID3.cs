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
        if (windPrefab == null)
        {
            Debug.LogWarning("ID3のWind Prefabが設定されていません");
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

        // プレイヤー → マウスの方向
        Vector2 direction =
            (worldPos - transform.position).normalized;

        // 向いている角度
        float angle =
            Mathf.Atan2(direction.y, direction.x)
            * Mathf.Rad2Deg;

        // 風をプレイヤー位置に生成
        GameObject wind = Instantiate(
            windPrefab,
            transform.position,
            Quaternion.Euler(0f, 0f, angle - 90)
        );

        // WindWaveに移動情報を渡す
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
    }
}