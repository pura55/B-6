using UnityEngine;

public class Bullet : MonoBehaviour
{
<<<<<<< HEAD
    [SerializeField] public int damage = 1;
=======
    [Header("通常ダメージ")]
    [SerializeField] private int damage = 1;
>>>>>>> take

    [Header("ヒット時")]
    [SerializeField] private float hitScale = 3f;

    [Header("エフェクト切り替え")]
    [SerializeField] private GameObject normalEffect;
    [SerializeField] private GameObject hitEffect;

    [Header("着弾後の範囲ダメージ")]
    [SerializeField] private int hitDamage = 1;
    [SerializeField] private float damageInterval = 1f;
    [SerializeField] private float hitDuration = 5f;
    [SerializeField] private float damageRadius = 1.5f;

    private float nextDamageTime;

    private bool hasHit = false;
    private bool isDamageArea = false;

    private void Update()
    {
        if (!isDamageArea)
            return;

        if (Time.time < nextDamageTime)
            return;

        Collider2D[] enemies =
            Physics2D.OverlapCircleAll(
                transform.position,
                damageRadius
            );

        foreach (Collider2D enemy in enemies)
        {
            if (!enemy.CompareTag("Enemy"))
                continue;

            EnemyDamaged health =
                enemy.GetComponent<EnemyDamaged>();

            if (health != null)
            {
                health.ReceiveDamage(hitDamage);
            }
        }

        nextDamageTime =
            Time.time + damageInterval;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit)
            return;

        // 壁に当たった
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
            return;
        }

        // 敵に当たった
        if (other.CompareTag("Enemy"))
        {
            hasHit = true;
            isDamageArea = true;

            // 通常エフェクトOFF
            if (normalEffect != null)
            {
                normalEffect.SetActive(false);
            }

            // ヒットエフェクトON
            if (hitEffect != null)
            {
                hitEffect.SetActive(true);
            }

            // 最初の着弾ダメージ
            EnemyDamaged health =
                other.GetComponent<EnemyDamaged>();

            if (health != null)
            {
                health.ReceiveDamage(damage);
            }

            // 弾を停止
            Rigidbody2D rb =
                GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity =
                    Vector2.zero;
            }

            // 大きくする
            transform.localScale =
                new Vector3(
                    hitScale,
                    hitScale,
                    transform.localScale.z
                );

            // 継続ダメージ開始時間
            nextDamageTime =
                Time.time + damageInterval;

            // 一定時間後に削除
            Destroy(
                gameObject,
                hitDuration
            );
        }
    }
}