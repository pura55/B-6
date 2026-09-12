using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// エンドスクリーン
/// 
/// 終了用スクリーンクラス
/// </summary>
public class EndScreen : ScreenBase
{
    #region State
    private bool isEndPressed = false; // 終了ボタンのフラグ
    private bool isNoPressed = false; // Noボタンのフラグ
    private Image image; // スクリーンの背景
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        startPosition = new Vector3(0f, 1080f, 0f);
        endPosition = Vector3.zero; 
    }

    void Update()
    {
        ChangeScreen();
    }

    /// @brief スクリーンを変更する関数
    private void ChangeScreen()
    {
        if(isEndPressed)
        {
            myRectTransform.anchoredPosition = Vector3.MoveTowards(myRectTransform.anchoredPosition, endPosition, changeSpeed * Time.deltaTime);

            if(myRectTransform.anchoredPosition.y == endPosition.y)
            {
                SetImageActive(true);
                isEndPressed = false;
            }
        }

        if(isNoPressed)
        {
            myRectTransform.anchoredPosition = Vector3.MoveTowards(myRectTransform.anchoredPosition, startPosition, changeSpeed * Time.deltaTime);

            if (myRectTransform.anchoredPosition.y == startPosition.y)
            {
                isNoPressed = false;
            }
        }
    }

    /// @brief 終了ボタンフラグを設定する関数
    public void SetEndPressed()
    {
        isEndPressed = true; 
    }

    /// @brief Noボタンフラグを設定する関数
    public void SetNoPressed()
    {
        isNoPressed = true;
        SetImageActive(false);
    }

    /// @brief スクリーンの背景を設定する関数
    private void SetImageActive(bool active)
    {
        image.enabled = active;
    }
}
