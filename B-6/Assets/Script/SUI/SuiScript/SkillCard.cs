using UnityEngine;
using TMPro;

public class SkillCard : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descText;

    private string skillName;
    private LevelUpUI ui;

    public void Setup(
        string name,
        string desc,
        LevelUpUI levelUI)
    {
        skillName = name;
        ui = levelUI;

        nameText.text = name;
        descText.text = desc;
    }

    public void OnClick()
    {
        ui.SelectSkill(skillName);
    }
}