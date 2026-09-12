using UnityEngine;

/// <summary>
/// Yesボタン
/// 
/// ゲーム終了に承諾するボタン
/// </summary>
public class YesButton : ButtonBase
{
    public override void OnClick()
    {
        EndGame();
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
