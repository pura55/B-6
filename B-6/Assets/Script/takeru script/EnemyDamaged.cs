using UnityEngine;

public class EnemyDamaged : MonoBehaviour
{
    [Header("敵のHP")]
    [SerializeField] private int enemyHp = 5;

    private static int playerKillCount = 0;
    private bool playerKillCounted = false;

    public static int GetPlayerKillCount()
    {
        return playerKillCount;
    }

    public static void ResetPlayerKillCount()
    {
        playerKillCount = 0;
    }

    public void ReceiveDamage(int damage)
    {
        enemyHp -= damage;

        Debug.Log($"<color=blue>{gameObject.name} に {damage} ダメージ！ </color>残りHP：{enemyHp}");

        if (enemyHp <= 0)
        {
            Die();
        }
    }

    public void ReceivePlayerDamage(int damage)
    {
        enemyHp -= damage;

        Debug.Log($"<color=blue>{gameObject.name} に {damage} ダメージ！ </color>残りHP：{enemyHp}");

        if (enemyHp <= 0)
        {
            if (!playerKillCounted)
            {
                playerKillCount++;
                playerKillCounted = true;
            }

            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} を倒した！");
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Rock"))
        {
            Destroy(gameObject);
        }
    }
}