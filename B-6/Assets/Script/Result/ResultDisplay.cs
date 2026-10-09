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
        // ゲームの時間を通常に戻す
        Time.timeScale = 1f;

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
            enemyDownText.text = $"{kills}";
        }

        // プレイ時間（ボス撃破時に保存された時間）
        float time = GameTimer.GetFinalElapsedTime();

        Debug.Log("リザルトで取得した時間：" + time + "秒");

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        if (timeText != null)
        {
            timeText.text = $"{minutes:0}:{seconds:00}";
        }

        // 防いだ落石の数
        int blockedRocks = RockHit.GetBlockedRockCount();

        if (blockedRockText != null)
        {
            blockedRockText.text = $"{blockedRocks}";
        }
    }

    // 獲得スキルの画像を表示
    void ShowAcquiredSkills()
    {
        if (skillIconParent == null || skillIconPrefab == null)
        {
            Debug.LogWarning("スキル画像の表示設定が不足しています！");
            return;
        }

        // シーンをまたいで保持している獲得スキル一覧
        List<SkillData> skills = AcquiredSkillData.acquiredSkills;

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

            Sprite selectedIcon = null;

            switch (characterID)
            {
                case 1:
                    selectedIcon = skill.resultCharacter1Icon;
                    break;
                case 2:
                    selectedIcon = skill.resultCharacter2Icon;
                    break;
                case 3:
                    selectedIcon = skill.resultCharacter3Icon;
                    break;
                case 4:
                    selectedIcon = skill.resultCharacter4Icon;
                    break;
                default:
                    Debug.LogWarning("キャラクターIDが不正です：" + characterID);
                    break;
            }

            // キャラクター専用アイコンがなければ共通アイコンを使用
            if (selectedIcon == null)
            {
                selectedIcon = skill.resultIcon;
            }

            // 画像がない場合は生成しない
            if (selectedIcon == null)
            {
                Debug.LogWarning(
                    "リザルト用アイコン未設定: " +
                    skill.skillName +
                    " / キャラクターID: " +
                    characterID
                );
                continue;
            }

            // アイコン生成
            Image icon = Instantiate(skillIconPrefab, skillIconParent);
            icon.sprite = selectedIcon;
            icon.enabled = true;
        }
    }
}