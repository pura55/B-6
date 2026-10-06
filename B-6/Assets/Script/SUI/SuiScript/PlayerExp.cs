using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExp : MonoBehaviour
{
    public Slider expSlider;

    public int level = 1;

    public int maxExp = 100;
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
        // =========================
        // EXP MAXサウンド設定
        // =========================
        if (soundPlayer != null)
        {
            soundPlayer.SetOneShot(expMaxSound);
        }

        // =========================
        // XP Sliderを探す
        // =========================
        if (expSlider == null)
        {
            GameObject expObject =
                GameObject.Find("Expber");

            if (expObject != null)
            {
                expSlider =
                    expObject.GetComponent<Slider>();
            }
        }

        // =========================
        // LevelUpUIを探す
        // =========================
        if (levelUpUI == null)
        {
            levelUpUI =
                FindFirstObjectByType<LevelUpUI>();
        }


        // =========================
        // XPバー初期設定
        // =========================
        if (expSlider != null)
        {
            expSlider.minValue = 0;
            expSlider.maxValue = maxExp;
            expSlider.value = currentExp;
        }
        else
        {
            Debug.LogWarning(
                "ExpSliderが見つかりません"
            );
        }

        if (levelUpUI == null)
        {
            Debug.LogWarning(
                "LevelUpUIが見つかりません"
            );
        }
    }


    public void AddExp(int amount)
    {
        if (isLevelUp)
            return;

        currentExp += amount;

        if (expSlider != null)
        {
            expSlider.value = currentExp;
        }

        if (currentExp >= maxExp)
        {
            // =========================
            // EXP MAXサウンド
            // =========================
            if (soundPlayer != null)
            {
                soundPlayer.PlayOneShot();
            }

            StartCoroutine(
                LevelUpAnimation()
            );
        }
    }


    IEnumerator LevelUpAnimation()
    {
        isLevelUp = true;

        if (expSlider != null)
        {
            expSlider.value = maxExp;
        }

        yield return new WaitForSeconds(1f);

        int remainExp =
            currentExp - maxExp;

        level++;

        Debug.Log(
            "Level Up!! Lv." + level
        );

        maxExp += 50;

        currentExp =
            remainExp;

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