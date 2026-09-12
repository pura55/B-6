using UnityEngine;
using UnityEngine.InputSystem;

public class SkillID1 : MonoBehaviour
{
    [Header("キャラ1 スキル")]
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 5f;

    [Header("スキルストック")]
    [SerializeField] private SkillStock skillStock;

    public void UseSkill()
    {
        // ストックが無ければ発動しない
        if (skillStock == null)
        {
            Debug.LogWarning("SkillStockが設定されていません");
            return;
        }

        if (!skillStock.UseStock())
        {
            return;
        }

        if (bulletPrefab == null)
        {
            Debug.LogWarning("ID1のBullet Prefabが設定されていません");
            return;
        }

        if (Mouse.current == null || Camera.main == null)
            return;

        Vector3 mousePos = Mouse.current.position.ReadValue();

        mousePos.z =
            -Camera.main.transform.position.z;

        Vector3 worldPos =
            Camera.main.ScreenToWorldPoint(mousePos);

        worldPos.z = 0f;

        Vector3 direction =
            (worldPos - transform.position).normalized;

        float angle =
            Mathf.Atan2(direction.y, direction.x)
            * Mathf.Rad2Deg;

        GameObject bullet = Instantiate(
            bulletPrefab,
            transform.position,
            Quaternion.Euler(0f, 0f, angle)
        );

        Rigidbody2D rb2D =
            bullet.GetComponent<Rigidbody2D>();

        if (rb2D != null)
        {
            rb2D.linearVelocity =
                direction * speed;
        }

        Destroy(bullet, lifeTime);

        Debug.Log(
            "<color=red>【ID1 スキル発動】</color>"
        );
    }
}