using UnityEngine;
using UnityEngine.InputSystem;

public class SkillID1 : MonoBehaviour
{
    [Header("キャラ1 スキル")]
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] public float CT = 3f;

    //スキルの弾数
    [SerializeField] private int bulletCount = 1;

    [Header("スキルストック")]
    [SerializeField] private SkillStock skillStock;

    //弾数を増やす
    public void AddBulletCount(int value)
    {
        bulletCount += value;

        Debug.Log(
            $"<color=cyan>スキル弾数アップ！</color> {bulletCount}発"
        );
    }

    public bool UseSkill()
    {
        if (skillStock == null)
        {
            Debug.LogWarning("SkillStockが設定されていません");
            return false;
        }

        if (bulletPrefab == null)
        {
            Debug.LogWarning("ID1のBullet Prefabが設定されていません");
            return false;
        }

        if (Mouse.current == null || Camera.main == null)
            return false;

        // 実際に発動できるときだけストック消費
        if (!skillStock.UseStock())
            return false;

        Vector3 mousePos =
            Mouse.current.position.ReadValue();

        mousePos.z =
            -Camera.main.transform.position.z;

        Vector3 worldPos =
            Camera.main.ScreenToWorldPoint(mousePos);

        worldPos.z = 0f;

        Vector3 direction =
            (worldPos - transform.position).normalized;

        Debug.Log("現在の弾数：" + bulletCount);

        float angle =
            Mathf.Atan2(direction.y, direction.x)
            * Mathf.Rad2Deg;


        //弾数分だけ発射
        for (int i = 0; i < bulletCount; i++)
        {
            float offsetAngle = 0f;

            //複数発なら扇状にする
            if (bulletCount > 1)
            {
                //角度調整
                float totalSpread = 30f;

                offsetAngle =
                    Mathf.Lerp(
                        -totalSpread / 2f,
                        totalSpread / 2f,
                        (float)i / (bulletCount - 1)
                    );
            }

            Vector3 bulletDirection =
                Quaternion.Euler(
                    0f,
                    0f,
                    offsetAngle
                ) * direction;


            GameObject bullet = Instantiate(
                bulletPrefab,
                transform.position,
                Quaternion.Euler(
                    0f,
                    0f,
                    angle + offsetAngle
                )
            );

            Rigidbody2D rb2D =
                bullet.GetComponent<Rigidbody2D>();

            if (rb2D != null)
            {
                rb2D.linearVelocity =
                    bulletDirection * speed;
            }

            Destroy(bullet, lifeTime);
        }


        Debug.Log(
            "<color=red>【ID1 スキル発動】</color>"
        );

        return true;
    }
}
