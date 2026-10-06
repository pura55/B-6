using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// セレクトエクスクラメーション
/// 
/// 未開放キャラ選択時に注意する処理を行います
/// </summary>
public class SelectedExclamation : MonoBehaviour
{
    #region Config
   [Header("DECREASING ALPHA")]
   [SerializeField]private float decreaseVelocity = 0.4f; // αの減少速度
    #endregion

    #region State
    private Image myImage; // このオブジェクトのイメージ
    private Image childImage; // 子のオブジェクトのイメージ
    private TextMeshProUGUI childText; // テキスト
    private float myColor = 1; // 色
    private float settingAlpha = 1; // α値を設定する際の値
    #endregion

    void Start()
    {
        myImage = GetComponent<Image>();
        childImage = transform.GetChild(0).GetComponent<Image>();
        childText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        myImage.color = new Color(myColor, myColor, myColor, 0); // 始まりはα値 = 0
        childImage.color = myImage.color;
        childText.color = myImage.color;
    }

    void Update()
    {
        if((myImage.color.a) <= 0)
        {
            return;
        }

        DecreaseAlpha();
    }

    /// @brief α値を減少させる関数
    private void DecreaseAlpha()
    {
        float alpha = myImage.color.a;

        alpha -= decreaseVelocity * Time.deltaTime;

        myImage.color = new Color(myColor, myColor, myColor, alpha);
        childImage.color = myImage.color;
        childText.color = myImage.color;
    }

    /// @brief α値を設定する関数
    public void SetAlpha()
    {
        myImage.color = new Color(myColor, myColor, myColor, settingAlpha);
    }
}
