using UnityEngine;

/// <summary>
/// スタートスクリーン
/// 
/// 開始用スクリーンクラス
/// </summary>
public class StartScreen : ScreenBase
{
    #region State
    protected bool isPlayPressed = false; // プレイボタンのフラグ
    protected bool isBackPressed = false; // 戻るボタンのフラグ
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRectTransform = GetComponent<RectTransform>();
        endPosition = new Vector3(-1920f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        PressPlayButton();

        PressBackButton();
    }

    /// @brief プレイボタンが押された後の処理を行う関数
    protected void PressPlayButton()
    {
        if (isPlayPressed)
        {
            myRectTransform.anchoredPosition = Vector3.MoveTowards(myRectTransform.anchoredPosition, endPosition, changeSpeed * Time.deltaTime);

            if (myRectTransform.anchoredPosition.x == endPosition.x)
            {
                isPlayPressed = false;
            }
        }
    }

    /// @brief 戻るボタンが押された後の処理を行う関数
    protected void PressBackButton()
    {
        if (isBackPressed)
        {
            myRectTransform.anchoredPosition = Vector3.MoveTowards(myRectTransform.anchoredPosition, startPosition, changeSpeed * Time.deltaTime);

            if (myRectTransform.anchoredPosition.x == startPosition.x)
            {
                isBackPressed = false;
            }
        }
    }

    /// @brief プレイボタンフラグを設定する関数
    public void SetPlayPressed()
    {
        isPlayPressed = true;
    }

    /// @brief 戻るボタンフラグを設定する関数
    public void SetBackPressed()
    {
        isBackPressed = true;
    }
}
