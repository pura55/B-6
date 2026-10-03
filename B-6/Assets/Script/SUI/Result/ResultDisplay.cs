
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ResultDisplay : MonoBehaviour
{
    public TMP_Text enemyDownText;
    public TMP_Text timeText;
    public TMP_Text blockedRockText;

    // 獲得スキル表示
    public Transform skillIconParent;
    public Image skillIconPrefab;

    // キャラクター選択IDのScriptableObject
    [Header("キャラクター選択")]
    [SerializeField] private SelectCharacterID selectCharacterID;

    void Start()
    {
        ShowResult();
        ShowAcquiredSkills();
    }

    void ShowResult()
    {
        // 討伐数
        int kills = EnemyHealth.GetPlayerKillCount()
                  + EnemyDamaged.GetPlayerKillCount();

        if (enemyDownText != null)
        {
            enemyDownText.text = $"{kills}体";
        }

        // プレイ時間
        float time = GameTimer.GetFinalElapsedTime();

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        if (timeText != null)
        {
            timeText.text = $"{minutes:00}分{seconds:00}秒";
        }

        // 防いだ落石の数
        int blockedRocks = RockHit.GetBlockedRockCount();

        if (blockedRockText != null)
        {
            blockedRockText.text = $"{blockedRocks}個";
        }
    }

    // 獲得スキルの画像を表示
    void ShowAcquiredSkills()
    {
        if (skillIconParent == null || skillIconPrefab == null)
        {
            Debug.LogWarning(
                "スキル画像の表示設定が不足しています！");
            return;
        }

        // シーンをまたいで保持している獲得スキル一覧
        List<SkillData> skills =
            AcquiredSkillData.acquiredSkills;

        Debug.Log("獲得スキルの登録数：" + skills.Count);

        // ScriptableObjectからキャラクターIDを取得
        int characterID = selectCharacterID != null
            ? selectCharacterID.GetSelectID()
            : 0;

        Debug.Log("選択中のキャラクターID：" + characterID);

        foreach (SkillData skill in skills)
        {
            if (skill == null)
                continue;

            // キャラクター別アイコンを選択
            Sprite selectedIcon = null;

            switch (characterID)
            {
                case 1:
                    selectedIcon = skill.character1Icon;
                    break;

                case 2:
                    selectedIcon = skill.character2Icon;
                    break;

                case 3:
                    selectedIcon = skill.character3Icon;
                    break;

                case 4:
                    selectedIcon = skill.character4Icon;
                    break;

                default:
                    Debug.LogWarning(
                        "キャラクターIDが不正です：" + characterID);
                    break;
            }

            // キャラクター別アイコンがなければ共通アイコン
            if (selectedIcon == null)
            {
                selectedIcon = skill.icon;
            }

            // 画像がない場合は生成しない
            if (selectedIcon == null)
            {
                Debug.LogWarning(
                    "アイコン未設定: " + skill.skillName +
                    " / キャラクターID: " + characterID
                );

                continue;
            }

            // 画像がある場合だけアイコンを生成
            Image icon = Instantiate(
                skillIconPrefab,
                skillIconParent
            );

            icon.sprite = selectedIcon;
            icon.enabled = true;
        }
    }
}