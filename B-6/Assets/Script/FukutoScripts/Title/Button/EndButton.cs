using UnityEngine;

/// <summary>
/// エンドボタン
/// 
/// ゲーム終了ボタン
/// </summary>
public class EndButton : ButtonBase
{
    #region State
    [SerializeField] private EndScreen endScreen; // 終了スクリーン
    #endregion

    public override void OnClick()
    {
        endScreen.SetEndPressed();
    }


    /// @brief ゲームを終了させる関数
    private void EndGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
        Application.Quit();//ゲームプレイ終了
#endif
    }
}
