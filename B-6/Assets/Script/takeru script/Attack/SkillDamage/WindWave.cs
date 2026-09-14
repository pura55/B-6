using UnityEngine;

public class WindWave : MonoBehaviour
{
    [Header("ダメージ")]
    [SerializeField] public int damage = 1;

    [Header("風の拡大")]
    [SerializeField] private float startScale = 0.5f;
    [SerializeField] private float endScale = 2f;

    private Vector2 direction;

    private float speed;
    private float lifeTime;

    private float spawnTime;

    public void Setup(
        Vector2 moveDirection,
        float moveSpeed,
        float extinction
    )
    {
        // 発射方向
        direction = moveDirection.normalized;

        // 移動速度
        speed = moveSpeed;

        // 消えるまでの時間
        lifeTime = extinction;

        // 発射した時間
        spawnTime = Time.time;

        // 最初は小さい状態
        transform.localScale =
            Vector3.one * startScale;
    }

    private void Update()
    {
        // -------------------------
        // 前へ移動
        // -------------------------

        transform.position +=
            (Vector3)direction
            * speed
            * Time.deltaTime;


        // -------------------------
        // 小さい状態から大きくする
        // -------------------------

        float elapsedTime =
            Time.time - spawnTime;

        float rate =
            elapsedTime / lifeTime;

        rate = Mathf.Clamp01(rate);

        float currentScale =
            Mathf.Lerp(
                startScale,
                endScale,
                rate
            );

        transform.localScale =
            Vector3.one * currentScale;


        // -------------------------
        // 一定時間で消える
        // -------------------------

        if (elapsedTime >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 敵以外は無視
        if (!other.CompareTag("Enemy"))
            return;

        EnemyDamaged enemy =
            other.GetComponent<EnemyDamaged>();

        if (enemy != null)
        {
            enemy.ReceiveDamage(damage);
        }
    }
}