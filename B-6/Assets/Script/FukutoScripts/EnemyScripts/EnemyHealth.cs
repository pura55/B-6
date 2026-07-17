using UnityEngine;

public class EnemyDamaged : MonoBehaviour
{
    #region Config
    private int enemyHp =5;

    #endregion

    #region State

    #endregion 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Rock"))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PlayerAttack"))
        {
            ReciveDamage(col.gameObject);
        }
    }

    private void ReciveDamage(GameObject attack)
    {
        enemyHp--;
    }
}
