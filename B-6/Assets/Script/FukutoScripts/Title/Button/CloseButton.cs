using UnityEngine;

/// <summary>
/// クローズボタン
/// 
/// スクリーンを閉じるボタン
/// </summary>
public class CloseButton : ButtonBase
{
    #region Config
    [SerializeField] private GameObject targetScreen; // 対象のスクリーン
    #endregion

    public override void OnClick()
    {
        base.OnClick();
        targetScreen.SetActive(false);
    }
}
