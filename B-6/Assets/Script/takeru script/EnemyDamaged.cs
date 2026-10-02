using UnityEngine;

public class EnemyDamaged: MonoBehaviour
{
    [Header("敵のHP")]
    [SerializeField] private int enemyHp = 5;

    /// <summary>
    /// ダメージを受ける
    /// </summary>
    public void ReceiveDamage(int damage)
    {
        enemyHp -= damage;

        Debug.Log($"<color=blue>{gameObject.name} に {damage} ダメージ！ </color>残りHP：{enemyHp}");

        if (enemyHp <= 0)
        {
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

