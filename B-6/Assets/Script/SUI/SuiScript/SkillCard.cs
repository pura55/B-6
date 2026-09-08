using UnityEngine;
using UnityEngine.UI;

public class SkillCard : MonoBehaviour
{
    [Header("UI")]
    public Image iconImage;

    private SkillData skillData;
    private LevelUpUI ui;

    // カード表示設定
    public void Setup(
        SkillData data,
        LevelUpUI levelUI)
    {
        skillData = data;
        ui = levelUI;

        // アイコンを表示
        iconImage.sprite = data.icon;
    }

    // ボタンクリック時
    public void OnClick()
    {
        Debug.Log("Button Click");

        // nullチェック
        if (ui == null)
        {
            Debug.LogError("ui が null です！");
            return;
        }

        if (skillData == null)
        {
            Debug.LogError("skillData が null です！");
            return;
        }

        ui.SelectSkill(skillData);
    }
}