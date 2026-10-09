using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LevelUpUI : MonoBehaviour
{
    [Header("レベルアップUI")]
    [SerializeField] private GameObject levelUpPanel;


    [Header("カード")]
    public SkillCard card1;
    public SkillCard card2;
    public SkillCard card3;


    [Header("全スキル")]
    public SkillData[] allSkills;


    private PlayerExp playerExp;
    private SkillManager skillManager;

    private bool canSelect = false;


    // ========================================
    // レベルアップ画面を開く
    // ========================================
    public void Open(PlayerExp exp)
    {
        Debug.Log("========== LevelUpUI.Open ==========");

        Debug.Log(
            "LevelUpUIを持っているObject : " +
            gameObject.name
        );

        Debug.Log(
            "LevelUpUIの親 : " +
            (transform.parent != null
                ? transform.parent.name
                : "なし")
        );

        Debug.Log(
            "LevelUpPanel : " +
            (levelUpPanel != null
                ? levelUpPanel.name
                : "NULL")
        );

        Debug.Log(
            "LevelUpUI自身がActive : " +
            gameObject.activeInHierarchy
        );

        Debug.Log(
            "LevelUpPanelが設定されているか : " +
            (levelUpPanel != null)
        );


        playerExp = exp;


        // SkillManager取得
        if (skillManager == null)
        {
            skillManager =
                FindFirstObjectByType<SkillManager>();
        }

        if (skillManager == null)
        {
            Debug.LogError(
                "SkillManager が見つかりません！"
            );

            return;
        }


        // LevelUpPanel確認
        if (levelUpPanel == null)
        {
            Debug.LogError(
                "LevelUpPanel が設定されていません！"
            );

            return;
        }


        // LevelUpPanelを表示
        levelUpPanel.SetActive(true);


        canSelect = false;

        Time.timeScale = 0f;


        // スキル抽選
        List<SkillData> candidates =
            GetRandomSkills(3);


        // カード設定
        SetupCard(card1, candidates, 0);
        SetupCard(card2, candidates, 1);
        SetupCard(card3, candidates, 2);


        // 3秒後に選択可能
        StartCoroutine(
            EnableSelectAfterDelay()
        );
    }



    // ========================================
    // 3秒たってから操作可能
    // ========================================
    private IEnumerator EnableSelectAfterDelay()
    {
        yield return new WaitForSecondsRealtime(3f);

        canSelect = true;

        Debug.Log("レベルアップ操作可能");
    }


    // ========================================
    // カード設定
    // ========================================
    private void SetupCard(
        SkillCard card,
        List<SkillData> list,
        int index)
    {
        if (card == null)
        {
            Debug.LogError(
                "LevelUpUI：SkillCard が設定されていません！"
            );

            return;
        }


        if (index >= list.Count)
        {
            card.gameObject.SetActive(false);

            return;
        }


        SkillData data = list[index];


        if (data == null)
        {
            Debug.LogError(
                "LevelUpUI：SkillData が null です！"
            );

            card.gameObject.SetActive(false);

            return;
        }


        card.gameObject.SetActive(true);


        // 現在のスキルレベルを取得
        int level =
            skillManager.GetLevel(data.type) + 1;


        // スキルデータをカードに渡す
        card.Setup(
            data,
            this,
            level
        );
    }


    // ========================================
    // ランダム抽選
    // ========================================
    private List<SkillData> GetRandomSkills(int count)
    {
        List<SkillData> candidates =
            new List<SkillData>();


        Debug.Log("=== GetRandomSkills ===");

        Debug.Log(
            "allSkills : " +
            (allSkills != null)
        );

        Debug.Log(
            "skillManager : " +
            (skillManager != null)
        );


        // ========================================
        // allSkillsチェック
        // ========================================

        if (allSkills == null)
        {
            Debug.LogError(
                "LevelUpUI：allSkills が null！"
            );

            return candidates;
        }


        // ========================================
        // SkillManagerチェック
        // ========================================

        if (skillManager == null)
        {
            Debug.LogError(
                "LevelUpUI：skillManager が null！"
            );

            return candidates;
        }


        // ========================================
        // MAX以外を候補に入れる
        // ========================================

        foreach (SkillData skill in allSkills)
        {
            if (skill == null)
            {
                Debug.LogError(
                    "LevelUpUI：allSkills の中に " +
                    "null な SkillData があります！"
                );

                continue;
            }


            if (!skillManager.IsMax(skill.type))
            {
                candidates.Add(skill);
            }
        }


        // ========================================
        // シャッフル
        // ========================================

        for (int i = 0;
             i < candidates.Count;
             i++)
        {
            int r =
                Random.Range(
                    i,
                    candidates.Count
                );


            SkillData temp =
                candidates[i];

            candidates[i] =
                candidates[r];

            candidates[r] =
                temp;
        }


        // ========================================
        // 先頭からcount個取得
        // ========================================

        List<SkillData> result =
            new List<SkillData>();


        for (int i = 0;
             i < count &&
             i < candidates.Count;
             i++)
        {
            result.Add(
                candidates[i]
            );
        }


        Debug.Log(
            "抽選されたスキル数：" +
            result.Count
        );


        return result;
    }


    // ========================================
    // カード選択
    // ========================================
    public void SelectSkill(SkillData data)
    {
        // ========================================
        // 操作可能か確認
        // ========================================

        if (!canSelect)
        {
            Debug.Log(
                "まだ選択できません"
            );

            return;
        }


        // ========================================
        // SkillDataチェック
        // ========================================

        if (data == null)
        {
            Debug.LogError(
                "SelectSkill：SkillData が null です！"
            );

            return;
        }


        // ========================================
        // SkillManagerチェック
        // ========================================

        if (skillManager == null)
        {
            skillManager =
                FindFirstObjectByType<SkillManager>();
        }


        if (skillManager == null)
        {
            Debug.LogError(
                "SelectSkill：SkillManager が見つかりません！"
            );

            return;
        }


        // ========================================
        // スキルレベルアップ
        // ========================================

        skillManager.LevelUp(data);


        Debug.Log(
            data.skillName +
            " を取得！"
        );


        // ========================================
        // LevelUpPanelを閉じる
        // ========================================

        if (levelUpPanel != null)
        {
            levelUpPanel.SetActive(false);
        }


        // ========================================
        // ゲーム再開
        // ========================================

        Time.timeScale = 1f;


        // ========================================
        // PlayerExpへ通知
        // ========================================

        if (playerExp != null)
        {
            playerExp.FinishLevelUp();
        }
        else
        {
            Debug.LogError(
                "LevelUpUI：playerExp が null です！"
            );
        }
    }
}
