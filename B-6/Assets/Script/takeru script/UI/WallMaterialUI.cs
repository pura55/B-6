using UnityEngine;
using TMPro;

public class WallMaterialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI materialText;

    [SerializeField] private ItemData wallMaterial;

    [SerializeField] private int maxMaterial = 10;


    void Update()
    {
        if (PartyManager.Instance == null)
            return;

        if (wallMaterial == null)
        {
            Debug.LogError("WallMaterialUIÇÃWall MaterialÇ™ñ¢ê›íËÇ≈Ç∑");
            return;
        }

        int count = PartyManager.Instance.GetItemCount(wallMaterial);

        materialText.text =
            "ï«ëfçﬁÅF" + count + " / " + maxMaterial;
    }
}