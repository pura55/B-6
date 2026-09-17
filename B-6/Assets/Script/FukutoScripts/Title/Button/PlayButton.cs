using UnityEngine;

public class PlayButton : ButtonBase
{
    #region State
    [SerializeField] StartScreen startScreen; // 開始スクリーン
    [SerializeField] SelectScreen selectScreen; // 開始スクリーン
    #endregion

    public override void OnClick()
    {
        startScreen.SetPlayPressed();
        selectScreen.SetPlayPressed();
    }
}
