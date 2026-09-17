using UnityEngine;
using UnityEngine.InputSystem;

public class SkillID4 : MonoBehaviour
{
    [Header("キャラ4 ブリンクスキル")]
    [SerializeField] private GameObject blinkAttackPrefab;

    [Header("ブリンク距離")]
    [SerializeField] private float blinkDistance = 3f;

    [Header("スキルストック")]
    [SerializeField] private SkillStock skillStock;

    public void UseSkill()
    {
        if (skillStock == null)
        {
            Debug.LogWarning("SkillStockが設定されていません");
            return;
        }

        if (blinkAttackPrefab == null)
        {
            Debug.LogWarning("BlinkAttack Prefabが設定されていません");
            return;
        }

        if (Mouse.current == null || Camera.main == null)
            return;

        if (!skillStock.UseStock())
            return;

        // マウス位置を取得
        Vector3 mousePos =
            Mouse.current.position.ReadValue();

        mousePos.z =
            -Camera.main.transform.position.z;

        Vector3 worldPos =
            Camera.main.ScreenToWorldPoint(mousePos);

        worldPos.z = 0f;

        // プレイヤーからマウス方向
        Vector2 direction =
            (worldPos - transform.position).normalized;

        // BlinkAttack Prefab生成
        GameObject blinkAttack = Instantiate(
            blinkAttackPrefab,
            transform.position,
            Quaternion.identity
        );

        // Prefab側のBlinkAttack取得
        BlinkAttack blink =
            blinkAttack.GetComponent<BlinkAttack>();

        if (blink != null)
        {
            blink.Setup(
                transform,
                direction,
                blinkDistance
            );
        }

        Debug.Log(
            "<color=purple>【ID4 ブリンクスキル発動】</color>"
        );
    }
}