using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2 : MonoBehaviour
{
    [SerializeField] private PlayerSpawner playerSpawner;

    private Transform playerTr;

    [SerializeField] Vector2 cameraMaxPos = new Vector2(5f, 5f);
    [SerializeField] Vector2 cameraMinPos = new Vector2(-5f, -5f);

    private void Update()
    {
        // PlayerSpawner‚ª¶¬‚µ‚½Player‚ğæ“¾
        if (playerTr == null)
        {
            if (playerSpawner == null)
                return;

            GameObject player = playerSpawner.GetPlayer();

            // ‚Ü‚¾¶¬‚³‚ê‚Ä‚¢‚È‚¯‚ê‚ÎŸ‚ÌƒtƒŒ[ƒ€‚Ü‚Å‘Ò‚Â
            if (player == null)
                return;

            playerTr = player.transform;

            Debug.Log("ƒJƒƒ‰‚ªPlayer‚ğæ“¾‚µ‚Ü‚µ‚½");
        }

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