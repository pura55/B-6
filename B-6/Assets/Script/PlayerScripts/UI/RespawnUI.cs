using UnityEngine;
using UnityEngine.UI;

public class RespawnUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image respawnImage;
    [SerializeField] private Vector3 offset;

    [SerializeField] private float fillSpeed = 1f;
    private float currentFillAmount = 0f;

    void Update()
    {
        if (playerHealth == null || respawnImage == null)
            return;

        respawnImage.enabled = playerHealth.IsDead;

        respawnImage.fillAmount =
            playerHealth.GetRespawnTimeRate();

        transform.position =
         playerHealth.transform.position + offset;
    }

    public void SetPlayer(PlayerHealth health)
    {
        playerHealth = health;
    }
}