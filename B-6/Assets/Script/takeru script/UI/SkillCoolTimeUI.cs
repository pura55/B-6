using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTimeUI : MonoBehaviour
{
    [Header("DATA")]
    [SerializeField] private PlayerProgressData playerProgressData;

    [Header("スキルストック")]
    [SerializeField] private SkillStock skillStock;

    [Header("UI")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image coolTimeImage;

    [Header("IDごとのスキル画像")]
    [SerializeField] private Sprite skillIcon1;
    [SerializeField] private Sprite skillIcon2;
    [SerializeField] private Sprite skillIcon3;
    [SerializeField] private Sprite skillIcon4;


    private void Start()
    {
        // スポナーで生成されたSkillStockを自動検索
        FindSkillStock();

        // キャラIDによってスキル画像設定
        SetSkillIcon();
    }


    private void Update()
    {
        // SkillStockがまだ見つかっていない場合は再検索
        if (skillStock == null)
        {
            FindSkillStock();

            if (skillStock == null)
                return;
        }


        if (coolTimeImage == null)
            return;


        // =========================
        // クールタイム表示
        // =========================

        if (skillStock.IsCoolTime())
        {
            coolTimeImage.fillAmount =
                1f - skillStock.GetCoolTimeRate();
        }
        else
        {
            coolTimeImage.fillAmount = 1f;
        }
    }


    // =========================
    // SkillStockを自動検索
    // =========================

    private void FindSkillStock()
    {
        skillStock =
            FindObjectOfType<SkillStock>();


        if (skillStock != null)
        {
            Debug.Log(
                "<color=cyan>" +
                "SkillStockを自動取得しました" +
                "</color>"
            );
        }
    }


    // =========================
    // キャラIDによって画像変更
    // =========================

    private void SetSkillIcon()
    {
        if (playerProgressData == null)
        {
            Debug.LogWarning(
                "PlayerProgressDataが設定されていません"
            );

            return;
        }


        Sprite selectedIcon = null;


        switch (playerProgressData.id)
        {
            case 1:
                selectedIcon = skillIcon1;
                break;

            case 2:
                selectedIcon = skillIcon2;
                break;

            case 3:
                selectedIcon = skillIcon3;
                break;

            case 4:
                selectedIcon = skillIcon4;
                break;

            default:
                Debug.LogWarning(
                    $"キャラID {playerProgressData.id} のスキル画像がありません"
                );
                return;
        }


        if (backgroundImage != null)
        {
            backgroundImage.sprite =
                selectedIcon;
        }


        if (coolTimeImage != null)
        {
            coolTimeImage.sprite =
                selectedIcon;
        }
    }
}
