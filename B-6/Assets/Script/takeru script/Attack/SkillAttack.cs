using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
            return;
        }

        EnemyDamaged enemy = other.GetComponent<EnemyDamaged>();

        if (enemy != null)
        {
            enemy.ReceiveDamage(damage);
            Destroy(gameObject);
        }
    }
}