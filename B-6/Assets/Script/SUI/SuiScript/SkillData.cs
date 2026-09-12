using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Game/Skill Data")]
public class SkillData : ScriptableObject
{
    public SkillType type;

    public string skillName;
    [TextArea]
    public string description;

    public Sprite icon;

    // Lv1, Lv2, Lv3 ‚Ì’l
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