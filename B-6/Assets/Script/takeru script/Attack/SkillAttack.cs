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

        // “G‚É“–‚½‚Á‚½ê‡
        if (other.CompareTag("Enemy"))
        {
            // “G‚Ì‘Ì—Í‚ÌQÆ‚ğæ“¾
            EnemyHealth health = other.GetComponent<EnemyHealth>();

            // ƒ_ƒ[ƒWˆ—
            if (health != null) health.ReceiveDamage(damage);

            // ’eŠÛ‚Ìíœ
            Destroy(gameObject);
        }
    }
}