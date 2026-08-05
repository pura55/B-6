using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExp : MonoBehaviour
{
    public Slider expSlider;

    public int level = 1;

    public int maxExp = 100;
    private int currentExp = 0;

    private bool isLevelUp = false;

    // レベルアップUI
    public LevelUpUI levelUpUI;

    void Start()
    {
        expSlider.minValue = 0;
        expSlider.maxValue = maxExp;
        expSlider.value = currentExp;
    }

    public void AddExp(int amount)
    {
        // レベルアップ中は取得しない
        if (isLevelUp)
            return;

        currentExp += amount;

        expSlider.value = currentExp;

        if (currentExp >= maxExp)
        {
            StartCoroutine(LevelUpAnimation());
        }
    }

    IEnumerator LevelUpAnimation()
    {
        isLevelUp = true;

        // バーMAX表示
        expSlider.value = maxExp;

        yield return new WaitForSeconds(1f);

        // 余り経験値
        int remainExp = currentExp - maxExp;

        // レベルアップ
        level++;

        Debug.Log("Level Up!! Lv." + level);

        // 次レベル必要経験値
        maxExp += 50;

        // バー更新
        currentExp = remainExp;

        expSlider.maxValue = maxExp;
        expSlider.value = currentExp;

        // レベルアップ画面を開く
        levelUpUI.Open(this);
    }

    // レベルアップ終了通知
    public void FinishLevelUp()
    {
        isLevelUp = false;
    }
}