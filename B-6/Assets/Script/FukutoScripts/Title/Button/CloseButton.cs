using UnityEngine;

/// <summary>
/// クローズボタン
/// 
/// 設定を閉じるボタン
/// </summary>
public class CloseButton : ButtonBase
{
    #region Config
    [SerializeField] private GameObject settingScreen; // 終了スクリーン
    #endregion

    public override void OnClick()
    {
        base.OnClick();
        settingScreen.SetActive(false);
    }
}
