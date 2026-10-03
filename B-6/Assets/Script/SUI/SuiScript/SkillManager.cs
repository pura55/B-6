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
    [SerializeField] private PlayerProgressData playerProgressData;
    [SerializeField] private SkillStock procount;

    // 生成されたPlayerから取得
    private SkillID1 skillID1;
    private SkillID2 skillID2;
    private SkillID3 skillID3;
    private SkillID4 skillID4;


    // スキルレベル管理
    private Dictionary<SkillType, int> skillLevels =
        new Dictionary<SkillType, int>();


    void Update()
    {
        // Lキーでテスト
        if (Keyboard.current != null &&
            Keyboard.current.lKey.wasPressedThisFrame)
        {
            if (testSkill != null)
            {
                LevelUp(testSkill);
            }
        }
    }


    // 生成されたPlayerを設定
    public void SetPlayer(GameObject player)
    {
        if (player == null)
        {
            Debug.LogWarning(
                "SkillManager：Playerが設定されていません"
            );

            return;
        }

        playerAttack =
            player.GetComponentInChildren<PlayerAttack>();

        move =
            player.GetComponentInChildren<MoveScript>();

        maxhpup =
            player.GetComponentInChildren<PlayerHealth>();

        skillID1 =
            player.GetComponentInChildren<SkillID1>();

        skillID2 =
            player.GetComponentInChildren<SkillID2>();

        skillID3 =
            player.GetComponentInChildren<SkillID3>();

        skillID4 =
            player.GetComponentInChildren<SkillID4>();

        Debug.Log(
            "<color=cyan>生成されたPlayerをSkillManagerに設定しました</color>"
        );

        Debug.Log(
            "<color=yellow>SkillID1 : " +
            (skillID1 != null) +
            "</color>"
        );

        Debug.Log(
            "<color=yellow>SkillID2 : " +
            (skillID2 != null) +
            "</color>"
        );

        Debug.Log(
            "<color=yellow>SkillID3 : " +
            (skillID3 != null) +
            "</color>"
        );

        Debug.Log(
            "<color=yellow>SkillID4 : " +
            (skillID4 != null) +
            "</color>"
        );
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
        if (data == null)
        {
            Debug.LogError("SkillData が設定されていません！");
            return;
        }

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
                    move.speed += value;
                    Debug.Log("移動速度 +" + value);
                }
                else
                {
                    Debug.LogError("move script が設定されていません！");
                }

                break;


            //PlayerSkillattack
            case SkillType.ProjectileCount:

                if (skillID1 != null)
                {
                    skillID1.AddBulletCount((int)value);
                }
                else
                {
                    Debug.LogError("SkillID1 が設定されていません！");
                }

                if (skillID2 != null)
                {
                    skillID2.AddBulletCount((int)value);
                }
                else
                {
                    Debug.LogError("SkillID2 が設定されていません！");
                }

                if (skillID3 != null)
                {
                    skillID3.AddBulletCount((int)value);
                }
                else
                {
                    Debug.LogError("SkillID3 が設定されていません！");
                }

                if (skillID4 != null)
                {
                    skillID4.AddBulletCount((int)value);
                }
                else
                {
                    Debug.LogError("SkillID4 が設定されていません！");
                }

                Debug.Log("スキルの弾の数 + " + value);

                break;


            case SkillType.SkillCooldown:

                if (playerProgressData != null)
                {
                    playerProgressData.skillCT -= value;
                    Debug.Log("スキルクールタイム -" + value);
                }
                else
                {
                    Debug.LogError("skillct が設定されていません！");
                }

                break;


            case SkillType.SkillPower:

                if (playerProgressData != null)
                {
                    playerProgressData.skillDmg += (int)value;
                    Debug.Log("スキル威力 +" + value);
                }
                else
                {
                    Debug.LogError("PlayerProgressData が設定されていません！");
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