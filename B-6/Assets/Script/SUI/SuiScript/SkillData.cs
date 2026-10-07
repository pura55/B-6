using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Game/Skill Data")]
public class SkillData : ScriptableObject
{
    public SkillType type;

    public string skillName;

    [TextArea]
    public string description;


    // ========================================
    // スキル選択・報酬画面用
    // ========================================

    // 共通アイコン
    // キャラクター専用画像がない場合に使用
    public Sprite icon;

    // キャラクターごとのアイコン
    public Sprite character1Icon;
    public Sprite character2Icon;
    public Sprite character3Icon;
    public Sprite character4Icon;


    // ========================================
    // リザルト画面用
    // ========================================

    // 共通リザルトアイコン
    // キャラクター専用リザルト画像がない場合に使用
    public Sprite resultIcon;

    // キャラクターごとのリザルト専用アイコン
    public Sprite resultCharacter1Icon;
    public Sprite resultCharacter2Icon;
    public Sprite resultCharacter3Icon;
    public Sprite resultCharacter4Icon;


    // ========================================
    // Lv1, Lv2, Lv3 の値
    // ========================================

    public float level1;
    public float level2;
    public float level3;


    public float GetValue(int level)
    {
        switch (level)
        {
            case 1:
                return level1;

            case 2:
                return level2;

            case 3:
                return level3;

            default:
                return 0f;
        }
    }

    /// @brief ステータスを設定する関数
    public void SetStat(SkillMasterData　data)
    {
        // レベルごとの数値を格納
        foreach(var entity in data.entities)
        {
            switch(entity.level)
            {
                case 1: // Lv.1
                    level1 = entity.stat;
                    break;
                case 2: // Lv.2
                    level2 = entity.stat;
                    break;
                case 3: // Lv.3
                    level3 = entity.stat;
                    break;
                default:
                    break;
            }
        }
    }
}