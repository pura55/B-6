using UnityEngine;
using TMPro;

public class WallMaterialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI materialText;
    [SerializeField] private ItemData wallMaterial;

    void Update()
    {
        if (PartyManager.Instance == null)
            return;

        if (wallMaterial == null)
            return;

        int count = PartyManager.Instance.GetItemCount(wallMaterial);

        materialText.text =
            $"ï«ëfçﬁ : {count}\n" +
            $"çÏÇÍÇÈï« : {count / 10}";
    }
}