using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("通常ダメージ")]
    [SerializeField] public int damage = 1;

    [Header("ヒット時")]
    [SerializeField] private float hitScale = 3f;

    [Header("エフェクト切り替え")]
    [SerializeField] private GameObject normalEffect;
    [SerializeField] private GameObject hitEffect;

    [Header("着弾後の範囲ダメージ")]
    [SerializeField] private float damageRadius = 1.5f;

    [Header("DATA")]
    [SerializeField] private PlayerProgressData playerProgressData;

    private bool hasHit = false;


    private void Start()
    {
        // プレイヤーデータからスキルダメージ取得
        if (playerProgressData != null)
        {
            damage = playerProgressData.skillDmg;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // すでに着弾していたら何もしない
        if (hasHit)
            return;


        // =========================
        // 壁に当たった
        // =========================

        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
            return;
        }


        // =========================
        // 敵に当たった
        // =========================

        if (!other.CompareTag("Enemy"))
            return;


        hasHit = true;


        Debug.Log(
            "<color=yellow>弾が敵に着弾！</color>"
        );


        // =========================
        // 通常エフェクトOFF
        // =========================

        if (normalEffect != null)
        {
            normalEffect.SetActive(false);
        }


        // =========================
        // ヒットエフェクトON
        // =========================

        if (hitEffect != null)
        {
            hitEffect.SetActive(true);
        }


        // =========================
        // 着弾した場所を中心に範囲検索
        // =========================

        Collider2D[] enemies =
            Physics2D.OverlapCircleAll(
                transform.position,
                damageRadius
            );


        Debug.Log(
            "範囲内に見つかったCollider数：" +
            enemies.Length
        );


        // =========================
        // 範囲内の敵全員にダメージ
        // =========================

        foreach (Collider2D enemyCollider in enemies)
        {
            if (!enemyCollider.CompareTag("Enemy"))
                continue;


            // Colliderが子オブジェクトにあっても
            // 親からEnemyHealthを探す
            EnemyHealth health =
                enemyCollider.GetComponentInParent<EnemyHealth>();


            if (health == null)
            {
                Debug.LogWarning(
                    "EnemyHealthが見つかりません：" +
                    enemyCollider.name
                );

                continue;
            }


            Debug.Log(
                "<color=orange>" +
                "スキル範囲ダメージ：" +
                enemyCollider.transform.root.name +
                " に " +
                damage +
                " ダメージ" +
                "</color>"
            );


            // プレイヤーのスキルによるダメージとして処理
            // → 敵を倒した場合、ゲームオーバーの討伐数に反映される
            health.ReceivePlayerDamage(damage);
        }


        // =========================
        // 弾を停止
        // =========================

        Rigidbody2D rb =
            GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }


        // =========================
        // ヒットエフェクトを大きくする
        // =========================

        transform.localScale =
            new Vector3(
                hitScale,
                hitScale,
                transform.localScale.z
            );


        // =========================
        // 一定時間後に削除
        // =========================

        Destroy(gameObject, 0.5f);
    }


    // Sceneビューで範囲を確認する
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            transform.position,
            damageRadius
        );
    }
}