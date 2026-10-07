using UnityEngine;

/// <summary>
/// スキルデータローダー
/// 
/// スキルデータを読み込みます
/// </summary>
public class SkillDataRegister : MonoBehaviour
{
    #region Config
    // スキルのJSONデータ
    [Header("JSON DATA")]
    [SerializeField] private TextAsset criticalPercent; // クリティカル率
    [SerializeField] private TextAsset damageCt; // ダメージクールタイム
    [SerializeField] private TextAsset damageUp; // ダメージUP
    [SerializeField] private TextAsset destroyRecovery; // 敵討伐時の回復
    [SerializeField] private TextAsset hpUp; // 最大Hpアップ
    [SerializeField] private TextAsset itemRange; // アイテムの収集範囲
    [SerializeField] private TextAsset respawnCt; // リスポーンクールタイム
    [SerializeField] private TextAsset skillCount; // スキルの数
    [SerializeField] private TextAsset skillCt; // スキルクールタイム
    [SerializeField] private TextAsset skillDamage; // スキルダメージ
    [SerializeField] private TextAsset speedUp; // スピードアップ
    [SerializeField] private TextAsset wallCt; // 壁のクールタイム

    // スキルのスクリプタブルオブジェクト
    [Header("SKILL SCRIPTABLE")]
    [SerializeField] private SkillData critRateUp; // クリティカル率
    [SerializeField] private SkillData attackCooldown; // ダメージクールタイム
    [SerializeField] private SkillData attackPowerUp; // ダメージUP
    [SerializeField] private SkillData killHeal; // 敵討伐時の回復
    [SerializeField] private SkillData maxHpUp; // 最大Hpアップ
    [SerializeField] private SkillData pickupRange; // アイテムの収集範囲
    [SerializeField] private SkillData respawnCooldonw; // スキルクールタイム
    [SerializeField] private SkillData projectileCount; // スキルの数
    [SerializeField] private SkillData skillCooldown; // スキルクールタイム
    [SerializeField] private SkillData skillPower; // スキルダメージ
    [SerializeField] private SkillData moveSpeed; // スピードアップ
    [SerializeField] private SkillData wallCooldown; // 壁のクールタイム
    #endregion

    #region State
    // スキルの名前
    private enum SkillName
    {
        CRITICAL = 1, // クリティカル
        DAMAGE_CT,    // クールタイム
        DAMAGE_UP,    // ダメージアップ
        HEAL,         // 回復
        HP_UP,        // 最大HP
        ITEM_RANGE,   // アイテムの収集範囲
        RESPAWN_CT,   // リスポーンのクールタイム
        SKILL_COUNT,  // スキルの数
        SKILL_CT,     // スキルのクールタイム
        SKILL_DAMAGE, // スキルダメージ
        SPEED_UP,     // スピードアップ
        WALL_CT,      // 壁のクールタイム
    }
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RegistSkillData();
    }

    /// @brief スキルのデータを登録する関数
    private void RegistSkillData()
    {
        for(int i = (int)SkillName.CRITICAL; i <= (int)SkillName.WALL_CT; i++)
        {
            switch(i)
            {
                case (int)SkillName.CRITICAL:
                    critRateUp.SetStat(LoadJson(criticalPercent));
                    break;

                case (int)SkillName.DAMAGE_CT:
                    attackCooldown.SetStat(LoadJson(damageCt));
                    break;

                case (int)SkillName.DAMAGE_UP:
                    attackPowerUp.SetStat(LoadJson(damageUp));
                    break;

                case (int)SkillName.HEAL:
                    killHeal.SetStat(LoadJson(destroyRecovery));
                    break;

                case (int)SkillName.HP_UP:
                    maxHpUp.SetStat(LoadJson(hpUp));
                    break;

                case (int)SkillName.ITEM_RANGE:
                    pickupRange.SetStat(LoadJson(itemRange));
                    break;

                case (int)SkillName.RESPAWN_CT:
                    respawnCooldonw.SetStat(LoadJson(respawnCt));
                    break;

                case (int)SkillName.SKILL_COUNT:
                    projectileCount.SetStat(LoadJson(skillCount));  
                    break;

                case (int)SkillName.SKILL_CT:
                    skillCooldown.SetStat(LoadJson(skillCt));
                    break;

                case (int)SkillName.SKILL_DAMAGE:
                    skillPower.SetStat(LoadJson(skillDamage));
                    break;

                case (int)SkillName.SPEED_UP:
                    moveSpeed.SetStat(LoadJson(speedUp));
                    break;

                case (int)SkillName.WALL_CT:
                    wallCooldown.SetStat(LoadJson(wallCt));
                    break;
            }
        }
    }

    /// @brief Jsonファイルを読み込む関数
    private SkillMasterData LoadJson(TextAsset textAsset)
    {
        if(textAsset != null)
        {
            string jsonString = textAsset.text;

            // クラスに変換
            SkillMasterData data = JsonUtility.FromJson<SkillMasterData>(jsonString);

            return data; 
        }

        Debug.LogError("Jsonデータが読み込まれいていません");
        return null;
    }
}
