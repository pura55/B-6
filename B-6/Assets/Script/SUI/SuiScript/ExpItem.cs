using UnityEngine;

public class ExpItem : MonoBehaviour
{
    public int expAmount = 10;
    public float moveSpeed = 5f;

    private Transform player;
    private PlayerExp playerExp;

    void Start()
    {
        GameObject obj =
            GameObject.FindGameObjectWithTag("Player");

        if (obj != null)
        {
            player = obj.transform;
            playerExp = obj.GetComponent<PlayerExp>();
        }
    }

    void Update()
    {
        if (player == null || playerExp == null)
            return;

        float distance =
            Vector2.Distance(
                transform.position,
                player.position
            );

        // プレイヤーの取得範囲を使用
        if (distance <= playerExp.pickupRange)
        {
            transform.position =
                Vector2.MoveTowards(
                    transform.position,
                    player.position,
                    moveSpeed * Time.deltaTime
                );
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
