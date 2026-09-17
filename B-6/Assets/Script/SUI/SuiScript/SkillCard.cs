using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillCard : MonoBehaviour
{
    [Header("UI")]
    public Image iconImage;

    [Header("レベル表示")]
    public TextMeshProUGUI levelText;

    [Header("New表示")]
    public TextMeshProUGUI newText;

    [Header("選択キャラクター")]
    public SelectCharacterID selectCharacterID;

    private SkillData skillData;
    private LevelUpUI ui;

    // カード表示設定
    public void Setup(
        SkillData data,
        LevelUpUI levelUI,
        int level)
    {
        skillData = data;
        ui = levelUI;

        // アイコンを表示
        int playerID = selectCharacterID.GetSelectID();

        Sprite characterIcon = null;

        switch (playerID)
        {
            case 1:
                characterIcon = data.character1Icon;
                break;

            case 2:
                characterIcon = data.character2Icon;
                break;

            case 3:
                characterIcon = data.character3Icon;
                break;

            case 4:
                characterIcon = data.character4Icon;
                break;
        }

        // キャラ専用画像があればそれを使用
        // なければ共通アイコンを使用
        if (characterIcon != null)
        {
            iconImage.sprite = characterIcon;
        }
        else
        {
            iconImage.sprite = data.icon;
        }

        // レベルを表示
        levelText.text = level.ToString();

        // 初めて取得するスキルに「New!」を表示
        if (level == 1)
        {
            newText.gameObject.SetActive(true);
        }
        else
        {
            newText.gameObject.SetActive(false);
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