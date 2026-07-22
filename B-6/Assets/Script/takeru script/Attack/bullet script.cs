using UnityEngine;
using UnityEngine.InputSystem;

public class bulletscript : MonoBehaviour
{
    public GameObject bulletPrefab;
    [SerializeField]private float speed = 10f;
    [SerializeField] private float time = 5;
    [SerializeField] private float CT = 3f;

    private float nextCoolLogTime = 0f;
    private float nextShotTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < nextShotTime) 
        {
            if (Time.time >= nextCoolLogTime)
            {
                Debug.Log($"<color=yellow>クールタイム中</color> 残り {nextShotTime - Time.time:F1}秒");
                nextCoolLogTime = Time.time + 1f;
            }
            return;

        }
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            Debug.Log($"<color=red>【スキル攻撃】</color>をした");

            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos.z = -Camera.main.transform.position.z;

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            worldPos.z = 0;

            Vector3 direction = (worldPos - transform.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb2D = bullet.GetComponent<Rigidbody2D>();

            if (rb2D != null)
            {
                rb2D.linearVelocity = direction * speed;
            }

            Destroy(bullet, time);

            // 次に撃てる時間を設定
            nextShotTime = Time.time + CT;
        }
    }

   
}
