using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
    [Header("UŒ‚")]
    [SerializeField] public int Attack = 1;
    [SerializeField] private float attackRange = 2.5f;

    [Range(0, 360)]
    [SerializeField] private float attackAngle = 120f;

    [SerializeField] public float attackTime = 0.3f;

    [Header("ƒNƒŠƒeƒBƒJƒ‹")]
    [Range(0f, 1f)]
    [SerializeField] public float criticalRate = 0.05f;

    [SerializeField] private float criticalDamageMultiplier = 2f;

    [Header("ƒŒƒCƒ„[")]
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

    [SerializeField]
    private PlayerProgressData playerProgressData;


    void Start()
    {
        playerAnimation = GetComponent<ID1Sprite>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerHealth = GetComponent<PlayerHealth>();

        Attack = playerProgressData.atkDmg;
    }


    void Update()
    {
        // =========================
        // €–S’†
        // =========================
        if (playerHealth != null && playerHealth.IsDead)
        {
            // UŒ‚“r’†‚Å€‚ñ‚¾ê‡‚àI—¹
            if (isAttacking)
            {
                EndAttack();
            }

            return;
        }


        // =========================
        // ’ÊíUŒ‚“ü—Í
        // =========================
        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame &&
            !isAttacking)
        {
            StartAttack();
        }


        // =========================
        // UŒ‚ˆ—
        // =========================
        if (isAttacking)
        {
            AttackMove();
        }
    }


    void StartAttack()
    {
        Debug.Log("y’ÊíUŒ‚ŠJnz");

        playerAnimation.ChangeState(
            ID1Sprite.PlayerAnimState.Attack
        );

        Vector3 mouseWorld =
            Camera.main.ScreenToWorldPoint(
                Mouse.current.position.ReadValue()
            );

        mouseWorld.z = 0;


        // =========================
        // UŒ‚‚µ‚½•ûŒü‚ğŒü‚­
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
        // •Çƒ`ƒFƒbƒN
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
                "•Ç‚É“–‚½‚Á‚ÄUŒ‚’†’f"
            );

            EndAttack();

            return;
        }


        // =========================
        // UŒ‚”ÍˆÍ
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
        // ƒ_ƒ[ƒW
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
                    $"yƒNƒŠƒeƒBƒJƒ‹Iz{target.name} ‚É {damage} ƒ_ƒ[ƒW"
                );
            }
            else
            {
                Debug.Log(
                    $"yƒqƒbƒgz{target.name} ‚É {damage} ƒ_ƒ[ƒW"
                );
            }


            enemy.ReceiveDamage(damage);
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

        Debug.Log("y’ÊíUŒ‚I—¹z");
    }


    // MoveScript‚©‚çUŒ‚’†‚©Šm”F
    public bool IsAttacking()
    {
        return isAttacking;
    }
}