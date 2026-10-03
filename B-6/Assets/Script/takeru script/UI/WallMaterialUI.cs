using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WallMaterialUI : MonoBehaviour
{
    [Header("データ")]
    [SerializeField] private ItemData wallMaterial;

    // Inspectorから設定しなくても自動取得
    private WallSkill wallSkill;


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
        // 最初にWallSkillを探す
        FindWallSkill();
    }


    void Update()
    {
        // =================================
        // WallSkillを自動検索
        // =================================

        if (wallSkill == null)
        {
            FindWallSkill();

            // まだ見つかっていなければ終了
            if (wallSkill == null)
                return;
        }


        // =================================
        // PartyManager確認
        // =================================

        if (PartyManager.Instance == null)
            return;


        if (wallMaterial == null)
            return;


        // =================================
        // 素材数取得
        // =================================

        int materialCount =
            PartyManager.Instance.GetItemCount(
                wallMaterial
            );


        int required =
            wallSkill.RequiredMaterial;


        // =================================
        // 壁を出せる枚数
        // =================================

        if (required > 0)
        {
            int wallCount =
                materialCount / required;


            if (wallCount != oldWallCount)
            {
                UpdateWallIcons(wallCount);

                oldWallCount =
                    wallCount;
            }
        }


        // =================================
        // クールタイム
        // =================================

        if (coolTimeImage != null)
        {
            if (wallSkill.IsCoolTime())
            {
                // CT中
                coolTimeImage.fillAmount =
                    1f -
                    wallSkill.GetCoolTimeRate();
            }
            else
            {
                // CT終了
                coolTimeImage.fillAmount =
                    1f;
            }
        }


        // =================================
        // 数字表示
        // =================================

        if (materialText != null)
        {
            materialText.text =
                $"壁素材数 : {materialCount}/10";
        }
    }


    /// <summary>
    /// WallSkillを自動検索
    /// </summary>
    private void FindWallSkill()
    {
        // Unity 6 / 新しいUnity向け
        wallSkill =
            FindFirstObjectByType<WallSkill>();


        if (wallSkill != null)
        {
            Debug.Log(
                "WallSkillを自動取得しました : " +
                wallSkill.gameObject.name
            );

            // 新しいWallSkillを取得したので
            // アイコン更新をやり直す
            oldWallCount = -1;
        }
    }


    /// <summary>
    /// 壁アイコン更新
    /// </summary>
    private void UpdateWallIcons(int wallCount)
    {
        if (iconParent == null ||
            wallIconPrefab == null)
        {
            return;
        }


        // =================================
        // 今あるアイコンを全部削除
        // =================================

        for (int i = iconParent.childCount - 1;
             i >= 0;
             i--)
        {
            Destroy(
                iconParent.GetChild(i).gameObject
            );
        }


        // =================================
        // 必要な枚数だけ作る
        // =================================

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
