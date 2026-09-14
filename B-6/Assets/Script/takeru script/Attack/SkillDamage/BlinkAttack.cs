using UnityEngine;

public class BlinkAttack : MonoBehaviour
{
    [Header("範囲攻撃")]
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private int damage = 2;

    [Header("敵レイヤー")]
    [SerializeField] private LayerMask enemyLayer;

    [Header("Prefabの生存時間")]
    [SerializeField] private float lifeTime = 1f;

    private Transform player;
    private Vector2 direction;
    private float blinkDistance;


    public void Setup(
        Transform playerTransform,
        Vector2 moveDirection,
        float distance
    )
    {
        player = playerTransform;
        direction = moveDirection.normalized;
        blinkDistance = distance;

        // ブリンク実行
        Blink();

        // 一定時間後Prefab削除
        Destroy(gameObject, lifeTime);
    }


    private void Blink()
    {
        if (player == null)
            return;

        // 移動先
        Vector3 targetPosition =
            player.position
            + (Vector3)direction * blinkDistance;

        // プレイヤー瞬間移動
        player.position = targetPosition;

        // このPrefabも移動先へ
        transform.position = targetPosition;

        // 移動先で範囲攻撃
        AttackAround();
    }


    private void AttackAround()
    {
        Collider2D[] enemies =
            Physics2D.OverlapCircleAll(
                transform.position,
                attackRadius,
                enemyLayer
            );

        foreach (Collider2D hit in enemies)
        {
            EnemyDamaged enemy =
                hit.GetComponent<EnemyDamaged>();

            if (enemy != null)
            {
                enemy.ReceiveDamage(damage);
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(
            transform.position,
            attackRadius
        );
    }
}