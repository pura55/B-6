using UnityEngine;

public class WaterWave : MonoBehaviour
{
    [Header("ダメージ")]
    [SerializeField] public int damage = 1;

    [Header("ノックバック")]
    [SerializeField] private float knockBackPower = 3f;

    private Transform player;

    private Vector2 direction;

    private float speed;
    private float lifeTime;
    private float waterRange;

    private float spawnTime;

    public void Setup(
        Transform playerTransform,
        Vector2 moveDirection,
        float moveSpeed,
        float extinction,
        float range
    )
    {
        player = playerTransform;

        direction = moveDirection.normalized;

        speed = moveSpeed;
        lifeTime = extinction;
        waterRange = range;

        spawnTime = Time.time;
    }

    private void Update()
    {
        // 波を移動
        transform.position +=
            (Vector3)direction
            * speed
            * Time.deltaTime;

        // 時間で消滅
        if (Time.time - spawnTime >= lifeTime)
        {
            Destroy(gameObject);
            return;
        }

        // プレイヤーから一定距離離れたら消滅
        if (player != null)
        {
            float distance =
                Vector2.Distance(
                    player.position,
                    transform.position
                );

            if (distance >= waterRange)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 壁
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
            return;
        }

        // 敵
        if (other.CompareTag("Enemy"))
        {
            // ダメージ
            EnemyDamaged health =
                other.GetComponent<EnemyDamaged>();

            if (health != null)
            {
                health.ReceiveDamage(damage);
            }

            // ノックバック
            Rigidbody2D enemyRb =
                other.GetComponent<Rigidbody2D>();

            if (enemyRb != null)
            {
                enemyRb.AddForce(
                    direction * knockBackPower,
                    ForceMode2D.Impulse
                );
            }
        }
    }
}