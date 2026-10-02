using UnityEngine;

public class Camera2 : MonoBehaviour
{
    [SerializeField] private PlayerSpawner playerSpawner;

    private Transform playerTr;

    [SerializeField]
    Vector2 cameraMaxPos = new Vector2(5f, 5f);

    [SerializeField]
    Vector2 cameraMinPos = new Vector2(-5f, -5f);


    private void Start()
    {
        // PlayerSpawnerŠm”F
        if (playerSpawner == null)
        {
            Debug.LogWarning(
                "PlayerSpawner‚ªİ’è‚³‚ê‚Ä‚¢‚Ü‚¹‚ñ"
            );

            return;
        }

        // Spawner‚ª¶¬‚µ‚½Player‚ğæ“¾
        GameObject player =
            playerSpawner.GetPlayer();

        if (player != null)
        {
            playerTr = player.transform;
        }
        else
        {
            Debug.LogWarning(
                "¶¬‚³‚ê‚½Player‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ"
            );
        }
    }


    private void Update()
    {
        // Player‚ª‚Ü‚¾‚¢‚È‚¢ê‡
        if (playerTr == null)
            return;


        float x = Mathf.Clamp(
            playerTr.position.x,
            cameraMinPos.x,
            cameraMaxPos.x
        );

        float y = Mathf.Clamp(
            playerTr.position.y,
            cameraMinPos.y,
            cameraMaxPos.y
        );


        transform.position = new Vector3(
            x,
            y,
            -10f
        );
    }
}