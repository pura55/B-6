using UnityEngine;
using UnityEngine.UI;

public class PlayerExp : MonoBehaviour
{
    public Slider expSlider;

    public int currentExp = 0;
    public int maxExp = 100;

    public int level = 1;


    void Start()
    {
        expSlider.maxValue = maxExp;
        expSlider.value = currentExp;
    }


    // 経験値を取得した時に呼ぶ
    public void AddExp(int amount)
    {
        currentExp += amount;


        // レベルアップ処理
        while (currentExp >= maxExp)
        {
            currentExp -= maxExp;  // 超過分を残す

            LevelUp();
        }


        // バー更新
        expSlider.value = currentExp;
    }


    void LevelUp()
    {
        level++;

        Debug.Log("レベルアップ！ Lv." + level);


        // 必要経験値を増やす例
        maxExp += 50;

        expSlider.maxValue = maxExp;
    }
}