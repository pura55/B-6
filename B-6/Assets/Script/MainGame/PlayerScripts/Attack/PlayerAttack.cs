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
    [Range(0f, 1f)]
    [SerializeField] public float criticalRate = 0.05f;

    [SerializeField] private float criticalDamageMultiplier = 2f;

    [Header("レイヤー")]
    public LayerMask enemyLayer;
    public LayerMask wallLayer;

    private bool isAttacking;
    private float attackTimer;

    private Vector2 attackDirection;
    private float startAngle;
    private float currentAngle;

    private ID1Sprite playerAnimation;
    private SpriteRenderer spriteRenderer;
    private PlayerHealth playerHealth;

    private HashSet<EnemyHealth> hitEnemies =
        new HashSet<EnemyHealth>();

    [SerializeField] private PlayerProgressData playerProgressData;



    void Start()
    {
        playerAnimation = GetComponent<ID1Sprite>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerHealth = GetComponent<PlayerHealth>();

        // データの設定
        Attack = playerProgressData.atkDmg;
        attackTime = playerProgressData.atkCT;
    }


    void Update()
    {
        // =========================
        // 死亡中
        // =========================
        if (playerHealth != null && playerHealth.IsDead)
        {
            // 攻撃途中で死んだ場合も終了
            if (isAttacking)
            {
                EndAttack();
            }

            return;
        }


        // =========================
        // 通常攻撃入力
        // =========================
        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame &&
            !isAttacking)
        {
            StartAttack();
        }


        // =========================
        // 攻撃処理
        // =========================
        if (isAttacking)
        {
            AttackMove();
        }
    }


    void StartAttack()
    {
        playerAnimation.ChangeState(
            ID1Sprite.PlayerAnimState.Attack
        );

        Vector3 mouseWorld =
            Camera.main.ScreenToWorldPoint(
                Mouse.current.position.ReadValue()
            );

        mouseWorld.z = 0;


        // =========================
        // 攻撃した方向を向く
        // =========================

        if (mouseWorld.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }


        attackDirection =
            (mouseWorld - transform.position).normalized;


        startAngle =
            Mathf.Atan2(
                attackDirection.y,
                attackDirection.x
            )
            * Mathf.Rad2Deg
            - attackAngle / 2;


        currentAngle = startAngle;

        attackTimer = 0;

        hitEnemies.Clear();

        isAttacking = true;
    }


    void AttackMove()
    {
        attackTimer += Time.deltaTime;

        float progress =
            attackTimer / attackTime;


        currentAngle =
            Mathf.Lerp(
                startAngle,
                startAngle + attackAngle,
                progress
            );


        Vector2 swordDirection =
            new Vector2(
                Mathf.Cos(
                    currentAngle * Mathf.Deg2Rad
                ),
                Mathf.Sin(
                    currentAngle * Mathf.Deg2Rad
                )
            );


        // =========================
        // 壁チェック
        // =========================

        RaycastHit2D wall =
            Physics2D.Raycast(
                transform.position,
                swordDirection,
                attackRange,
                wallLayer
            );


        if (wall.collider != null)
        {
            Debug.Log(
                "壁に当たって攻撃中断"
            );

            EndAttack();

            return;
        }


        // =========================
        // 攻撃範囲
        // =========================

        Vector2 attackStart =
            transform.position;


        Vector2 attackEnd =
            attackStart
            + swordDirection * attackRange;


        Vector2 attackPos =
            (attackStart + attackEnd) / 2f;


        Collider2D[] targets =
            Physics2D.OverlapBoxAll(
                attackPos,
                new Vector2(
                    attackRange,
                    1.0f
                ),
                currentAngle,
                enemyLayer
            );


        // =========================
        // ダメージ
        // =========================

        foreach (Collider2D target in targets)
        {
            EnemyHealth enemy =
                target.GetComponent<EnemyHealth>();

            if (enemy == null)
                continue;


            if (hitEnemies.Contains(enemy))
                continue;


            hitEnemies.Add(enemy);


            bool isCritical =
                Random.value <= criticalRate;


            int damage = Attack;


            if (isCritical)
            {
                damage =
                    Mathf.RoundToInt(
                        Attack
                        * criticalDamageMultiplier
                    );

                Debug.Log(
                    $"【クリティカル！】{target.name} に {damage} ダメージ"
                );
            }
            else
            {
                Debug.Log(
                    $"【ヒット】{target.name} に {damage} ダメージ"
                );
            }


            enemy.ReceivePlayerDamage(damage);

            if (enemy.IsDead())
            {
                playerHealth.KillHeal();
            }
        }


        if (progress >= 1)
        {
            EndAttack();
        }
    }


    void EndAttack()
    {
        isAttacking = false;

        hitEnemies.Clear();
    }


    // MoveScriptから攻撃中か確認
    public bool IsAttacking()
    {
        return isAttacking;
    }
}