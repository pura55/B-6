using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillManager : MonoBehaviour
{
    [Header("テスト用")]
    public SkillData testSkill;

    [Header("参照")]
    [SerializeField] private PlayerAttack playerAttack;
    //[SerializeField] private PlayerAttack attackTime;
    //[SerializeField] private PlayerAttack criticalRate;
    [SerializeField] private MoveScript move;
    [SerializeField] private PlayerExp expItem;
    [SerializeField] private WallSkill wall;
    [SerializeField] private PlayerHealth maxhpup;
    [SerializeField] private Bullet skillpower;
    [SerializeField] private SkillID1 skillct;




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

            case SkillType.AttackCooldown:

                if (playerAttack != null)
                {
                    playerAttack.attackTime -= value;
                    Debug.Log("攻撃間隔 -" + value);
                }
                else
                {
                    Debug.LogError("AttackCoolDown が設定されていません！");
                }
                
                break;

            case SkillType.CritRate:

                if (playerAttack != null)
                {
                    playerAttack.criticalRate += value;
                    Debug.Log("クリティカル率 +" + value);
                }
                else
                {
                    Debug.LogError("CritRateUp が設定されていません！");
                }

                break;

            case SkillType.MaxHP:

                if (maxhpup != null)
                {
                    maxhpup.maxHP += value;
                    Debug.Log("最大HP +" + value);
                }
                else
                {
                    Debug.LogError("PlayerHealth が設定されていません！");
                }

                break;

            case SkillType.MoveSpeed:

                if (move != null)
                {
                    move .speed += value;
                    Debug.Log("移動速度 +" + value);
                }
                else
                {
                    Debug.LogError("move script が設定されていません！");
                }

                break;

            //PlayerSkillattack
            /*case SkillType.ProjectileCount:

            break;*/

            case SkillType.SkillCooldown:

                if (skillct != null)
                {
                    skillct.CT -= value;
                    Debug.Log("スキルクールタイム -" + value);
                }
                else
                {
                    Debug.LogError("skillct が設定されていません！");
                }

                break;

            case SkillType.SkillPower:

                if (skillpower != null)
                {
                    skillpower.damage += (int)value;
                    Debug.Log("スキル威力 +" + value);
                }
                else
                {
                    Debug.LogError("skillpower が設定されていません！");
                }

                break;

            //Playersupport
            case SkillType.KillHeal:

                if (maxhpup != null)
                {
                    maxhpup.killHeal += (int)value;
                    Debug.Log("敵を倒したときHP回復 +" + value);
                }
                else
                {
                    Debug.LogError("killHeal が設定されていません！");
                }

                break;

            case SkillType.PickupRange:

                if (expItem != null)
                {
                    expItem.pickupRange += value;
                    Debug.Log("取得範囲 +" + value);
                }
                else
                {
                    Debug.LogError("ExpItem が設定されていません！");
                }

                break;

            case SkillType.RespawnCooldown:

                if (maxhpup != null)
                {
                    maxhpup.respawnTime -= value;
                    Debug.Log("リスポーン時間 -" + value);
                }
                else
                {
                    Debug.LogError("respawnTime が設定されていません！");
                }

                break;

            case SkillType.WallCooldown:
                
                if (wall != null)
                {
                    wall.coolTime -= value;
                    Debug.Log("壁立てクールタイム -" + value);
                }
                else
                {
                    Debug.LogError("Wall が設定されていません！");
                }
                break;

            default:

                Debug.Log("まだ未実装: " + data.type);
                break;
        }
    }
}