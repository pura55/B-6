using UnityEngine;
using TMPro;

public class SkillCard : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI lvText;
    public TextMeshProUGUI descText;

    private SkillData skillData;
    private LevelUpUI ui;

    // カード表示設定
    public void Setup(
        SkillData data,
        int currentLevel,
        LevelUpUI levelUI)
    {
        skillData = data;
        ui = levelUI;

        nameText.text = data.skillName;
        descText.text = data.description;

        if (currentLevel == 0)
        {
            lvText.text = "NEW";
        }
        else
        {
            lvText.text = "Lv." + (currentLevel + 1);
        }
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