using UnityEngine;

/// <summary>
/// セッティングボタン
/// 
/// 設定ボタン
/// </summary>
public class SettingButton : ButtonBase
{
    #region Config
    [SerializeField] private GameObject settingScreen; // 設定スクリーン
    #endregion

    public override void OnClick()
    {
        base.OnClick();
        settingScreen.SetActive(true);
    }
}
