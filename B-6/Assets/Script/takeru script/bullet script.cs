using UnityEngine;
using UnityEngine.InputSystem;

public class bulletscript : MonoBehaviour
{
    public GameObject bulletPrefab;
    private float speed = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
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
        }
    }
}
