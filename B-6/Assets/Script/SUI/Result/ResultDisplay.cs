
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
        SkillManager skillManager =
            FindFirstObjectByType<SkillManager>();

        if (skillManager == null)
        {
            Debug.LogWarning(
                "SkillManager が見つかりません！");
            return;
        }

        if (skillIconParent == null || skillIconPrefab == null)
        {
            Debug.LogWarning(
                "スキル画像の表示設定が不足しています！");
            return;
        }

        List<SkillData> skills =
            skillManager.GetAcquiredSkills();

        // 選択中のキャラクターIDを取得
        SelectCharacterID selectCharacter =
            FindFirstObjectByType<SelectCharacterID>();

        int characterID = selectCharacter != null
            ? selectCharacter.GetSelectID()
            : 0;

        foreach (SkillData skill in skills)
        {
            if (skill == null) continue;

            // アイコンを生成
            Image icon = Instantiate(
                skillIconPrefab,
                skillIconParent
            );

            // キャラクター別アイコンを選択
            switch (characterID)
            {
                case 1:
                    icon.sprite = skill.character1Icon;
                    break;

                case 2:
                    icon.sprite = skill.character2Icon;
                    break;

                case 3:
                    icon.sprite = skill.character3Icon;
                    break;

                case 4:
                    icon.sprite = skill.character4Icon;
                    break;

                default:
                    icon.sprite = skill.icon;
                    break;
            }

            // キャラクター別アイコンが未設定なら
            // 共通アイコンを使用
            if (icon.sprite == null)
            {
                icon.sprite = skill.icon;
            }

            // 画像がある場合だけ表示
            icon.enabled = icon.sprite != null;
        }
    }
}