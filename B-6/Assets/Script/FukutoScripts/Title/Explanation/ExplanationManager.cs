using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// エクスプラネーションマネージャー
/// 
/// ゲーム説明スライドを管理します
/// </summary>
public class ExplanationManager : MonoBehaviour
{
    #region Config
    [SerializeField] private Sprite[] explanationSprites; // 説明のスプライト
    [SerializeField] private Image myImage;
    #endregion

    #region State
    private int currentElement = 0; // 現在の要素
    private int spriteLength = 0; // スプライト配列の長さ
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitValue();
    }

    private void InitValue()
    {
        myImage = GetComponent<Image>();
        spriteLength = explanationSprites.Length;
        myImage.sprite = explanationSprites[currentElement];
    }

    /// @brief 値をリセットする関数 
    public void ResetValue()
    {
        int zero = 0; // 0を代入するための変数
        currentElement = zero;
    }

    /// @brief 次の説明に移る処理を行う関数
    public void NextExplanation()
    {
        currentElement++;
        if(spriteLength <= currentElement)
        {
            int maxSprite = spriteLength;
            maxSprite--; // 最大値を求めるため配列サイズから減算

            currentElement = maxSprite;
        }

        myImage.sprite = explanationSprites[currentElement];
    }

    /// @brief 前の説明に移る処理を行う関数
    public void BackExplanation()
    {
        currentElement--;
        if (currentElement < 0) // 要素の最小値が0であるため、比較対象を0にする
        {
            int minSprite = 0; // 要素の最小値

            currentElement = minSprite;
        }

        myImage.sprite = explanationSprites[currentElement];
    }

 
}
