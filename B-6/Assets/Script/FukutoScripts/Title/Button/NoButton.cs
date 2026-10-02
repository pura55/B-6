using UnityEngine;

/// <summary>
/// エンドボタン
/// 
/// ゲーム終了ボタン
/// </summary>
public class NoButton : ButtonBase
{
    #region State
    [SerializeField] private EndScreen endScreen; // 終了スクリーン
    #endregion

    public override void OnClick()
    {
        endScreen.SetNoPressed();
    }
}
