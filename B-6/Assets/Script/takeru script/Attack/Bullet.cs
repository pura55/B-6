using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public int damage = 1;

    [Header("ƒqƒbƒg")]
    [SerializeField] private float hitScale = 3f;
    [SerializeField] private float destroyDelay = 0.3f;

    private bool hasHit = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit)
            return;

        // •Ç‚É“–‚½‚Á‚½
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
            return;
        }

        // “G‚É“–‚½‚Á‚½
        if (other.CompareTag("Enemy"))
        {
            hasHit = true;

            EnemyHealth health = other.GetComponent<EnemyHealth>();

            if (health != null)
            {
                health.ReceiveDamage(damage);
            }

            // ’e‚ÌˆÚ“®‚ğ~‚ß‚é
            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }

            // XEY‚ğ3‚É‚·‚é
            transform.localScale = new Vector3(
                hitScale,
                hitScale,
                transform.localScale.z
            );

            // ­‚µ‘Ò‚Á‚Ä‚©‚çíœ
            Destroy(gameObject, destroyDelay);
        }
    }
}