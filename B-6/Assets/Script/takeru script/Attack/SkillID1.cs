using UnityEngine;
using UnityEngine.InputSystem;

public class SkillID1 : MonoBehaviour
{
    [Header("キャラ1 スキル")]
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] public float CT = 3f;

    private float nextShotTime = 0f;

    public void UseSkill()
    {
        if (Time.time < nextShotTime)
        {
            Debug.Log(
                $"<color=yellow>クールタイム中</color> 残り {nextShotTime - Time.time:F1}秒"
            );

            return;
        }

        if (bulletPrefab == null)
        {
            Debug.LogWarning("ID1のBullet Prefabが設定されていません");
            return;
        }

        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = -Camera.main.transform.position.z;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;

        Vector3 direction =
            (worldPos - transform.position).normalized;

        GameObject bullet = Instantiate(
            bulletPrefab,
            transform.position,
            Quaternion.identity
        );

        Rigidbody2D rb2D = bullet.GetComponent<Rigidbody2D>();

        if (rb2D != null)
        {
            rb2D.linearVelocity = direction * speed;
        }

        Destroy(bullet, lifeTime);

        nextShotTime = Time.time + CT;

        Debug.Log("<color=red>【ID1 スキル発動】</color>");
    }
}