using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack2 : MonoBehaviour
{
    [Header("攻撃パラメーター")]
    [SerializeField] private int attack = 1;

    // 外部から攻撃力を変更できる
    public int Attack
    {
        get { return attack; }
        set { attack = value; }
    }

    public float attackRange = 2.5f;

    [Range(0, 360)]
    public float attackAngle = 120f;

    public LayerMask enemyLayer;
    public LayerMask wallLayer;

    void Update()
    {
        // 左クリックで攻撃
        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("通常攻撃");
            PerformSwipeAttack();
        }
    }

    void PerformSwipeAttack()
    {
        // マウス位置をワールド座標へ
        Vector3 mouseWorld =
            Camera.main.ScreenToWorldPoint(
                Mouse.current.position.ReadValue());

        mouseWorld.z = 0;

        // 攻撃方向
        Vector2 attackDirection =
            (mouseWorld - transform.position).normalized;

        transform.right = attackDirection;

        // 範囲内の敵を取得
        Collider2D[] targets =
            Physics2D.OverlapCircleAll(
                transform.position,
                attackRange,
                enemyLayer);

        foreach (var target in targets)
        {
            Vector2 directionToTarget =
                (target.transform.position - transform.position)
                .normalized;

            float angle =
                Vector2.Angle(
                    attackDirection,
                    directionToTarget);

            // 扇形判定
            if (angle > attackAngle / 2f)
                continue;

            // 壁判定
            RaycastHit2D wallHit =
                Physics2D.Raycast(
                    transform.position,
                    directionToTarget,
                    Vector2.Distance(
                        transform.position,
                        target.transform.position),
                    wallLayer);

            if (wallHit.collider != null)
            {
                Debug.Log("壁に遮られた");
                continue;
            }

            EnemyDamaged enemy =
                target.GetComponent<EnemyDamaged>();

            if (enemy != null)
            {
                enemy.ReceiveDamage(Attack);

                Debug.Log(
                    target.name + " に " +
                    Attack + " ダメージ");
            }
        }
    }

    // Sceneビューに攻撃範囲表示
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(
            transform.position,
            attackRange);
    }
}