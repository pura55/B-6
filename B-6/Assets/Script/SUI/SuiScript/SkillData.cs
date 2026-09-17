using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Game/Skill Data")]
public class SkillData : ScriptableObject
{
    public SkillType type;

    public string skillName;
    [TextArea]
    public string description;

    public Sprite icon;

    // キャラクターごとのアイコン
    public Sprite character1Icon;
    public Sprite character2Icon;
    public Sprite character3Icon;
    public Sprite character4Icon;

    // Lv1, Lv2, Lv3 の値
    public float level1;
    public float level2;
    public float level3;

    public float GetValue(int level)
    {
        switch (level)
        {
            case 1: return level1;
            case 2: return level2;
            case 3: return level3;
            default: return 0f;
        }
    }
}