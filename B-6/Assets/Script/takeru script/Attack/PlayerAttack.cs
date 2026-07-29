using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
    [Header("UŒ‚")]
    [SerializeField] private int Attack = 1;

    [SerializeField] private float attackRange = 2.5f;

    [Range(0, 360)]
    [SerializeField] private float attackAngle = 120f;

    [SerializeField] private float attackTime = 0.3f;


    [Header("ƒŒƒCƒ„[")]
    public LayerMask enemyLayer;
    public LayerMask wallLayer;


    private bool isAttacking;
    private float attackTimer;

    private Vector2 attackDirection;
    private float startAngle;
    private float currentAngle;


    // UŒ‚Ï‚İŠÇ—
    private HashSet<EnemyDamaged> hitEnemies = new HashSet<EnemyDamaged>();



    void Update()
    {
        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame &&
            !isAttacking)
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
        Debug.Log("y’ÊíUŒ‚ŠJnz");


        Vector3 mouseWorld =
            Camera.main.ScreenToWorldPoint(
                Mouse.current.position.ReadValue());

        mouseWorld.z = 0;


        attackDirection =
            (mouseWorld - transform.position).normalized;


        // UŒ‚ŠJnŠp“x
        startAngle =
            Mathf.Atan2(
                attackDirection.y,
                attackDirection.x)
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
                progress);



        Vector2 swordDirection =
            new Vector2(
                Mathf.Cos(currentAngle * Mathf.Deg2Rad),
                Mathf.Sin(currentAngle * Mathf.Deg2Rad));



        // •Çƒ`ƒFƒbƒN
        RaycastHit2D wall =
            Physics2D.Raycast(
                transform.position,
                swordDirection,
                attackRange,
                wallLayer);


        if (wall.collider != null)
        {
            Debug.Log("•Ç‚É“–‚½‚Á‚ÄUŒ‚’†’f");
            EndAttack();
            return;
        }



        // Œ•‚ÌˆÊ’u‚Å”»’è
        Vector2 attackPos =
            (Vector2)transform.position
            + swordDirection * attackRange;



        Collider2D[] targets =
            Physics2D.OverlapCircleAll(
                attackPos,
                0.5f,
                enemyLayer);



        foreach (Collider2D target in targets)
        {
            EnemyDamaged enemy =
                target.GetComponent<EnemyDamaged>();


            if (enemy == null)
                continue;


            if (hitEnemies.Contains(enemy))
                continue;



            hitEnemies.Add(enemy);


            Debug.Log(
                $"yƒqƒbƒgz{target.name}");


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
        Debug.Log("y’ÊíUŒ‚I—¹z");
    }
}