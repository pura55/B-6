using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExp : MonoBehaviour
{
    public Slider expSlider;

    public int level = 1;

    public int maxExp = 50;
    private int currentExp = 0;

    // 経験値取得範囲
    public float pickupRange = 3f;

    private bool isLevelUp = false;

    // レベルアップUI
    public LevelUpUI levelUpUI;

    // サウンド
    [SerializeField] private SoundPlayer soundPlayer;
    [SerializeField] private AudioClip expMaxSound;

    void Start()
    {
        // EXP MAXサウンド設定
        if (soundPlayer != null)
        {
            soundPlayer.SetOneShot(expMaxSound);
        }

        // XP Sliderを探す
        if (expSlider == null)
        {
            GameObject expObject = GameObject.Find("Expber");

            if (expObject != null)
            {
                expSlider = expObject.GetComponent<Slider>();
            }
        }

        // LevelUpUIを探す
        if (levelUpUI == null)
        {
            levelUpUI = FindFirstObjectByType<LevelUpUI>();
        }

        // XPバー初期設定
        if (expSlider != null)
        {
            expSlider.minValue = 0;
            expSlider.maxValue = maxExp;
            expSlider.value = currentExp;
        }
        else
        {
            Debug.LogWarning("ExpSliderが見つかりません");
        }

        if (levelUpUI == null)
        {
            Debug.LogWarning("LevelUpUIが見つかりません");
        }
    }

    public void AddExp(int amount)
    {
        if (isLevelUp)
            return;

        currentExp += amount;

        if (currentExp >= maxExp)
        {
            // 最大値で止めてゲージを満タンにする
            currentExp = maxExp;
            Debug.Log("現在の経験値: " + currentExp + " / " + maxExp);

            if (currentExp >= maxExp)
            {
                Debug.Log("レベルアップ条件を満たしました");
                expSlider.value = expSlider.maxValue;
            }

            // EXP MAXサウンド
            if (soundPlayer != null)
            {
                soundPlayer.PlayOneShot();
            }

            StartCoroutine(LevelUpAnimation());
        }
        else
        {
            if (expSlider != null)
            {
                expSlider.value = currentExp;
            }
        }
    }

    IEnumerator LevelUpAnimation()
    {
        isLevelUp = true;

        // ゲージを最大値に固定
        if (expSlider != null)
        {
            expSlider.maxValue = maxExp;
            expSlider.value = maxExp;
        }

        yield return new WaitForSeconds(1f);

        level++;

        Debug.Log("Level Up!! Lv." + level);

        // 取得済みスキル数を取得
        int acquiredSkillCount = 0;

        if (SkillManager.Instance != null)
        {
            acquiredSkillCount =
                SkillManager.Instance.GetAcquiredSkills().Count;
        }

        // 5種類取得するまでは+30、6種類目からは+50
        if (acquiredSkillCount <= 5)
        {
            maxExp += 30;
        }
        else
        {
            maxExp += 50;
        }

        // 次のレベルの経験値を初期化
        currentExp = 0;

        if (expSlider != null)
        {
            expSlider.maxValue = maxExp;
            expSlider.value = currentExp;
        }

        // レベルアップ画面
        if (levelUpUI != null)
        {
            levelUpUI.Open(this);
        }
    }

    public void FinishLevelUp()
    {
        isLevelUp = false;
    }
}