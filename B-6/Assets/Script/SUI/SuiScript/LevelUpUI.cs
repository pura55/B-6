using UnityEngine;
using System.Collections.Generic;

public class LevelUpUI : MonoBehaviour
{
    [Header("カード")]
    public SkillCard card1;
    public SkillCard card2;
    public SkillCard card3;

    [Header("全スキル")]
    public SkillData[] allSkills;

    private PlayerExp playerExp;
    private SkillManager skillManager;

    // Startより先に呼ばれる
    private void Awake()
    {
        skillManager = FindObjectOfType<SkillManager>();

        if (skillManager == null)
        {
            Debug.LogError("SkillManager が見つかりません！");
        }
    }

    // レベルアップ画面を開く
    public void Open(PlayerExp exp)
    {
        Debug.Log("LevelUpUI.Open が呼ばれた");

        playerExp = exp;

        gameObject.SetActive(true);

        // ゲーム停止
        Time.timeScale = 0f;

        // ランダムに3つ取得
        List<SkillData> candidates = GetRandomSkills(3);

        // カードへ設定
        SetupCard(card1, candidates, 0);
        SetupCard(card2, candidates, 1);
        SetupCard(card3, candidates, 2);
    }

    // カード設定
    private void SetupCard(
        SkillCard card,
        List<SkillData> list,
        int index)
    {
        if (index >= list.Count)
        {
            card.gameObject.SetActive(false);
            return;
        }

        SkillData data = list[index];

        int currentLevel = skillManager.GetLevel(data.type);

        card.gameObject.SetActive(true);

        card.Setup(data, currentLevel, this);
    }

    // ランダム抽選
    private List<SkillData> GetRandomSkills(int count)
    {
        List<SkillData> candidates =
            new List<SkillData>();

        // MAX以外を候補に入れる
        foreach (SkillData skill in allSkills)
        {
            if (!skillManager.IsMax(skill.type))
            {
                candidates.Add(skill);
            }
        }

        // シャッフル
        for (int i = 0; i < candidates.Count; i++)
        {
            int r = Random.Range(i, candidates.Count);

            SkillData temp = candidates[i];
            candidates[i] = candidates[r];
            candidates[r] = temp;
        }

        // 先頭からcount個取得
        List<SkillData> result =
            new List<SkillData>();

        for (int i = 0;
             i < count && i < candidates.Count;
             i++)
        {
            result.Add(candidates[i]);
        }

        return result;
    }

    // カード選択
    public void SelectSkill(SkillData data)
    {
        // 実際にレベルアップ
        skillManager.LevelUp(data);

        Debug.Log(data.skillName + " を取得！");

        // UIを閉じる
        gameObject.SetActive(false);

        // ゲーム再開
        Time.timeScale = 1f;

        // PlayerExpへ通知
        playerExp.FinishLevelUp();
    }
}