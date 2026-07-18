using UnityEngine;

public class MaterialItem : MonoBehaviour
{
    // E‚Á‚½‚É‚à‚ç‚¦‚é‘fŞ”
    public int materialAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMaterial player =
                other.GetComponent<PlayerMaterial>();

            if (player != null)
            {
                player.AddMaterial(materialAmount);
            }

            Destroy(gameObject);
        }
    }
}