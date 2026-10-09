using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// セレクトボタン
/// 
/// キャラクターを選択するボタンクラス
/// </summary>
public class SelectButton : ButtonBase
{
    #region Config
    [SerializeField] private bool toNext = false; // 次へ進めるかどうかのフラグ
    [SerializeField] private SelectAnimation selectAnimation; // 選択アニメーションクラス
    private const int maxId = 4;
    private const int minId = 1;
    #endregion

    #region State
    private ControlButtonImage myImage;
    [SerializeField] private ControlButtonImage cursorImage;
    [SerializeField] private SelectButton oppsiteButton; // 反対のボタン
    #endregion

    private void Start()
    {
        myImage = GetComponent<ControlButtonImage>();
    }

    public override void OnClick()
    {
        if (selectAnimation == null)
        {
            return;
        }

        base.OnClick();

        if (!toNext)
        {
            BackButton();
        }
        else
        {
            NextButton();
        }
    }

    /// @brief 選択を戻すボタンの処理を行う関数
    private void BackButton()
    {
        if (selectAnimation.GetCharacterID() != minId)
        {
            selectAnimation.BackToSprites();

            oppsiteButton.OnImage();

            // IDが最小値に達しているため画像オフ
            if (selectAnimation.GetCharacterID() == minId)
            {
                myImage.OffImage();
                cursorImage.OffImage();
            }
        }
    }

    /// @brief 選択を次に進めるボタンの処理を行う関数
    private void NextButton()
    {
        if (selectAnimation.GetCharacterID() != maxId)
        {
            selectAnimation.NextToSprites();

            oppsiteButton.OnImage();

            // IDが最大値に達しているため画像オフ
            if (selectAnimation.GetCharacterID() == maxId)
            {
                myImage.OffImage();
                cursorImage.OffImage();
            }
        }
    }

    /// @brief 逆側のボタンの画像を解放する関数
    public void OnImage()
    {
        if (myImage != null && cursorImage != null)
        {
            if(myImage.GetImageEnabled() == false && cursorImage.GetImageEnabled() == false)
            {
                myImage.OnImage();
                cursorImage.OnImage();
            }
        }
    }


}
