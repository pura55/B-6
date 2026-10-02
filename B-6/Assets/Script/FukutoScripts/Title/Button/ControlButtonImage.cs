using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// コントロールボタンイメージ
/// 
/// ボタンの画像を管理するクラス
/// </summary>
public class ControlButtonImage : MonoBehaviour
{

    private Image buttonImage; // このオブジェクトのイメージ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    /// @brief ボタンをオンにする関数
    public void OnImage()
    {
        buttonImage.enabled = true;
    }

    /// @brief ボタンをオフにする関数
    public void OffImage()
    {
        buttonImage.enabled = false;
    }

    /// @brief 画像が表示フラグを返す関数
    public bool GetImageEnabled() { return buttonImage.enabled; }
}
