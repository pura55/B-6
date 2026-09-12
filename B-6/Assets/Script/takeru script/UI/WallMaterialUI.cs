using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WallMaterialUI : MonoBehaviour
{
    [Header("データ")]
    [SerializeField] private ItemData wallMaterial;
    [SerializeField] private WallSkill wallSkill;

    [Header("クールタイム")]
    [SerializeField] private Image coolTimeImage;

    [Header("壁枚数アイコン")]
    [SerializeField] private Transform iconParent;
    [SerializeField] private GameObject wallIconPrefab;

    [Header("デバッグ表示")]
    [SerializeField] private TextMeshProUGUI materialText;

    private int oldWallCount = -1;

    void Start()
    {
     
    }

    void Update()
    {
        if (PartyManager.Instance == null)
            return;

        if (wallMaterial == null)
            return;

        if (wallSkill == null)
            return;

        int materialCount =
            PartyManager.Instance.GetItemCount(
                wallMaterial
            );

        int required =
            wallSkill.RequiredMaterial;

        // -------------------------
        // 壁を出せる枚数
        // -------------------------

        int wallCount =
            materialCount / required;

        if (wallCount != oldWallCount)
        {
            UpdateWallIcons(wallCount);

            oldWallCount = wallCount;
        }

        // -------------------------
        // クールタイム
        // -------------------------

        if (coolTimeImage != null)
        {
            bool cooling =
                wallSkill.IsCoolTime();

            coolTimeImage.gameObject.SetActive(
                cooling
            );

            if (cooling)
            {
                coolTimeImage.fillAmount =
                    wallSkill.GetCoolTimeRate();
            }
        }

        // -------------------------
        // 数字表示
        // -------------------------

        if (materialText != null)
        {
            materialText.text =
                $"壁素材 : {materialCount}";
        }
    }

    void UpdateWallIcons(int wallCount)
    {
        if (iconParent == null ||
            wallIconPrefab == null)
            return;

        // 今あるアイコンを全部削除
        for (int i = iconParent.childCount - 1;
             i >= 0;
             i--)
        {
            Destroy(
                iconParent.GetChild(i).gameObject
            );
        }

        // 必要な枚数だけ作る
        for (int i = 0;
             i < wallCount;
             i++)
        {
            Instantiate(
                wallIconPrefab,
                iconParent
            );
        }
    }
}