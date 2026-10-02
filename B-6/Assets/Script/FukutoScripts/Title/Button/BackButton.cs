using UnityEngine;

/// <summary>
/// バックボタン
/// 
/// 戻るボタン
/// </summary>
public class BackButton : ButtonBase
{
    #region State
    [SerializeField] private StartScreen startScreen;
    [SerializeField] private SelectScreen selectScreen;
    #endregion
    public override void OnClick()
    {
        selectScreen.SetBackPressed();
        startScreen.SetBackPressed();
    }
}
