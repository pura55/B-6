using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{

    [Header("攻撃パラメーター")]
    [SerializeField] public int Attack = 1;
    public float attackRange = 2.5f; //薙ぎ払いの届く距離
    [Range(0, 360)]
    public float attackAngle = 120f; //薙ぎ払いの角度

    public LayerMask enemyLayer;    //敵のレイヤー（設定用）
    public LayerMask wallLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // マウス右クリック、またはSpaceキーで攻撃を実行
        if ((Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame))
        {
            Debug.Log($"<color=green>【通常攻撃】</color>をした");
            PerformSwipeAttack();
        }

    }

    void PerformSwipeAttack()
    {
        // マウスの位置をワールド座標に変換
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorld.z = 0;

        // プレイヤーからマウスへの方向
        Vector2 attackDirection = (mouseWorld - transform.position).normalized;

        transform.right = attackDirection;

        // プレイヤーの周りの円形範囲にいるオブジェクトをすべて検知
        Collider2D[] targetsInMinimalRange = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        foreach (var target in targetsInMinimalRange)
        {
            Vector2 directionToTarget =
                (target.transform.position - transform.position).normalized;

            // 攻撃範囲（扇形）チェック
            float angleToTarget = Vector2.Angle(attackDirection, directionToTarget);

            if (angleToTarget >= attackAngle / 2f)
            {
                continue;
            }

            // 壁チェック
            RaycastHit2D wallHit = Physics2D.Raycast(
                transform.position,
                directionToTarget,
                Vector2.Distance(transform.position, target.transform.position),
                wallLayer);

            if (wallHit.collider != null)
            {
                Debug.Log("壁に遮られた");
                continue;
            }

            Debug.Log($"<color=red>【ヒット！】</color> {target.name} に攻撃！");

            EnemyDamaged enemy = target.GetComponent<EnemyDamaged>();

            if (enemy != null)
            {
                enemy.ReceiveDamage(Attack);
            }
        }

    }
}
