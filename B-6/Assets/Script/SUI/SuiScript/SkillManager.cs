using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillManager : MonoBehaviour
{
    [Header("テスト用")]
    public SkillData testSkill;

    [Header("参照")]
    [SerializeField] private PlayerAttack playerAttack;

    // スキルレベル管理
    private Dictionary<SkillType, int> skillLevels =
        new Dictionary<SkillType, int>();

    void Update()
    {
        // Lキーでテスト
        if (Keyboard.current != null &&
            Keyboard.current.lKey.wasPressedThisFrame)
        {
            LevelUp(testSkill);
        }
    }

    // 現在レベル取得
    public int GetLevel(SkillType type)
    {
        return skillLevels.ContainsKey(type)
            ? skillLevels[type]
            : 0;
    }

    // MAX判定
    public bool IsMax(SkillType type)
    {
        return GetLevel(type) >= 3;
    }

    // レベルアップ処理
    public void LevelUp(SkillData data)
    {
        int currentLevel = GetLevel(data.type);

        if (currentLevel >= 3)
        {
            Debug.Log(data.skillName + " はMAXです");
            return;
        }

        currentLevel++;
        skillLevels[data.type] = currentLevel;

        ApplySkill(data, currentLevel);

        Debug.Log(data.skillName + " Lv" + currentLevel);
    }

    // 効果適用
    private void ApplySkill(SkillData data, int level)
    {
        float value = data.GetValue(level);

        switch (data.type)
        {
            //Playerstatus
            case SkillType.AttackPower:

                if (playerAttack != null)
                {
                    playerAttack.Attack += (int)value;
                    Debug.Log("攻撃力 +" + value);
                }
                else
                {
                    Debug.LogError("PlayerAttack が設定されていません！");
                }

                break;

            /*case SkillType.AttackCooldown:

            break;*/

            /*case SkillType.CritRateUp:

            break;*/

            /*case SkillType.MaxHPUp:

            break;*/

            /*case SkillType.MoveSpeedUp:

            break;*/

            //PlayerSkillattack
            /*case SkillType.ProjectileCount:

            break;*/

            /*case SkillType.SkillCooldown:

            break;*/

            /*case SkillType.SkillPower:

            break;*/

            //Playersupport
            /*case SkillType.KillHeal:

            break;*/

            /*case SkillType.PickupRange:

            break;*/

            /*case SkillType.RespawnCooldown:

            break;*/

            /*case SkillType.WallCooldown:

            break;*/

            default:

                Debug.Log("まだ未実装: " + data.type);
                break;
        }
    }
}