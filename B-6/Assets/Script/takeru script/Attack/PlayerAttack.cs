using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
    [Header("攻撃")]
    [SerializeField] public int Attack = 1;

    [SerializeField] private float attackRange = 2.5f;

    [Range(0, 360)]
    [SerializeField] private float attackAngle = 120f;

    [SerializeField] public float attackTime = 0.3f;

    [Header("クリティカル")]
    /*
     0.05 → 5%
    0.10 → 10%
    0.25 → 25%
    0.50 → 50%
    1.00 → 100%
     */
    [Range(0f, 1f)]
    [SerializeField] public float criticalRate = 0.05f;//クリティカル率

    [SerializeField] private float criticalDamageMultiplier = 2f;//クリティカル倍率


    [Header("レイヤー")]
    public LayerMask enemyLayer;
    public LayerMask wallLayer;


    private bool isAttacking;
    private float attackTimer;

    private Vector2 attackDirection;
    private float startAngle;
    private float currentAngle;


    // 攻撃済み管理
    private HashSet<EnemyDamaged> hitEnemies = new HashSet<EnemyDamaged>();

    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame && !isAttacking)
        {
            StartAttack();
        }


        if (isAttacking)
        {
            AttackMove();
        }
    }


    void StartAttack()
    {
        Debug.Log("【通常攻撃開始】");


        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        mouseWorld.z = 0;


        attackDirection = (mouseWorld - transform.position).normalized;


        // 攻撃開始角度
        startAngle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg - attackAngle / 2;

        currentAngle = startAngle;


        attackTimer = 0;

        hitEnemies.Clear();

        isAttacking = true;
    }


    void AttackMove()
    {
        attackTimer += Time.deltaTime;

        float progress = attackTimer / attackTime;

        currentAngle = Mathf.Lerp(startAngle, startAngle + attackAngle, progress);


        Vector2 swordDirection = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad),
                Mathf.Sin(currentAngle * Mathf.Deg2Rad));


        // 壁チェック
        RaycastHit2D wall = Physics2D.Raycast(transform.position, swordDirection, attackRange, wallLayer);


        if (wall.collider != null)
        {
            Debug.Log("壁に当たって攻撃中断");
            EndAttack();
            return;
        }


        // 剣の位置で判定
        Vector2 attackPos = (Vector2)transform.position + swordDirection * attackRange;


        Collider2D[] targets = Physics2D.OverlapCircleAll(attackPos, 0.5f, enemyLayer);


        foreach (Collider2D target in targets)
        {
            EnemyDamaged enemy =
                target.GetComponent<EnemyDamaged>();


            if (enemy == null)
                continue;


            if (hitEnemies.Contains(enemy))
                continue;


            hitEnemies.Add(enemy);

            // クリティカル判定
            bool isCritical =
                Random.value <= criticalRate;


            int damage = Attack;


            if (isCritical)
            {
                damage =
                    Mathf.RoundToInt(
                        Attack * criticalDamageMultiplier);

                Debug.Log(
                    $"【クリティカル！】{target.name} に {damage} ダメージ");
            }
            else
            {
                Debug.Log(
                    $"【ヒット】{target.name} に {damage} ダメージ");
            }


            enemy.ReceiveDamage(Attack);
        }


        if (progress >= 1)
        {
            EndAttack();
        }
    }


    void EndAttack()
    {
        isAttacking = false;
        Debug.Log("【通常攻撃終了】");
    }
}