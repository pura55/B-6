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

    public bool UseSkill()
    {
        if (skillStock == null)
        {
            Debug.LogWarning("SkillStockが設定されていません");
            return false;
        }

        if (blinkAttackPrefab == null)
        {
            Debug.LogWarning("BlinkAttack Prefabが設定されていません");
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

        GameObject blinkAttack = Instantiate(
            blinkAttackPrefab,
            transform.position,
            Quaternion.identity
        );

        BlinkAttack blink =
            blinkAttack.GetComponent<BlinkAttack>();

        if (blink == null)
        {
            Debug.LogWarning(
                "BlinkAttack PrefabにBlinkAttack.csがありません"
            );

            Destroy(blinkAttack);
            return false;
        }

        blink.Setup(
            transform,
            direction,
            blinkDistance
        );

        Debug.Log(
            "<color=purple>【ID4 ブリンクスキル発動】</color>"
        );

        return true;
    }
}