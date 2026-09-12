using UnityEngine;

public class WallMaterial : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;


        if (PartyManager.Instance == null)
        {
            Debug.LogError("PartyManager‚ª‚ ‚è‚Ü‚¹‚ñ");
            return;
        }


        if (itemData == null)
        {
            Debug.LogError("WallMaterial‚ÌItemData‚ªİ’è‚³‚ê‚Ä‚¢‚Ü‚¹‚ñ");
            return;
        }


        PartyManager.Instance.AddItem(itemData, 1);

        Destroy(gameObject);
    }
}