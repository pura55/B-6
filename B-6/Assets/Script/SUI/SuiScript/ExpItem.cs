using UnityEngine;

public class ExpItem : MonoBehaviour
{
    public int expAmount = 10;

    public float pickupRange = 3f;
    public float moveSpeed = 5f;

    private Transform player;

    void Start()
    {
        GameObject obj =
            GameObject.FindGameObjectWithTag("Player");

        if (obj != null)
        {
            player = obj.transform;
        }
    }

    void Update()
    {
        if (player == null)
            return;

        float distance =
            Vector2.Distance(
                transform.position,
                player.position);

        // ‹z‚¢Šñ‚¹
        if (distance <= pickupRange)
        {
            transform.position =
                Vector2.MoveTowards(
                    transform.position,
                    player.position,
                    moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerExp exp =
                other.GetComponent<PlayerExp>();

            if (exp != null)
            {
                exp.AddExp(expAmount);
            }

            Destroy(gameObject);
        }
    }
}