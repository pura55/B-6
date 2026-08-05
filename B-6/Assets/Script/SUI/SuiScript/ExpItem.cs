using UnityEngine;

public class ExpItem : MonoBehaviour
{
    public int expAmount = 10;

    public float pickupRange = 3f;  // ‹z‚¢Šñ‚¹ŠJŽn‹——£
    public float moveSpeed = 5f;     // ‹z‚¢Šñ‚¹‘¬“x

    private Transform player;


    void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");

        if (obj != null)
        {
            player = obj.transform;
        }
    }


    void Update()
    {
        if (player == null)
            return;


        float distance = Vector2.Distance(
            transform.position,
            player.position
        );


        if (distance <= pickupRange)
        {
            transform.position = Vector2.MoveTowards(
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
            PlayerExp playerExp = other.GetComponent<PlayerExp>();

            if (playerExp != null)
            {
                playerExp.AddExp(expAmount);
            }

            Destroy(gameObject);
        }
    }
}