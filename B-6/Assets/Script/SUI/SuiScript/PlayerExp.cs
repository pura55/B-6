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


    void Start()
    {
        expSlider.minValue = 0;
        expSlider.maxValue = maxExp;
        expSlider.value = currentExp;
    }


    public void AddExp(int amount)
    {
        // レベルアップ演出中は取得しない
        if (isLevelUp)
            return;

        currentExp += amount;

        // バー更新
        expSlider.value = currentExp;

        // レベルアップ判定
        if (currentExp >= maxExp)
        {
            StartCoroutine(LevelUpAnimation());
        }
    }


    IEnumerator LevelUpAnimation()
    {
        isLevelUp = true;

        //------------------------------------------------
        //① バーを100%にする
        //------------------------------------------------

        expSlider.value = maxExp;

        //------------------------------------------------
        //② 0.3秒止める
        //------------------------------------------------

        yield return new WaitForSeconds(1f);

        //------------------------------------------------
        //③ 余った経験値を保存
        //------------------------------------------------

        int remainExp = currentExp - maxExp;

        //------------------------------------------------
        //④ レベルアップ
        //------------------------------------------------

        level++;

        Debug.Log("Level Up!!");

        //------------------------------------------------
        //⑤ 次のレベルの必要経験値
        //------------------------------------------------

        maxExp += 50;

        //------------------------------------------------
        //⑥ バーをリセット
        //------------------------------------------------

        currentExp = remainExp;

        expSlider.maxValue = maxExp;
        expSlider.value = currentExp;

        isLevelUp = false;
    }
}