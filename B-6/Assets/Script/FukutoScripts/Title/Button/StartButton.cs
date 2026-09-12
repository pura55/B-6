using UnityEngine;
using UnityEngine.SceneManagement; // 追加

/// <summary>
/// スタートボタン
/// 
/// ゲーム開始ボタン
/// </summary>
public class StartButton : ButtonBase
{
    public override void OnClick()
    {
        SceneManager.LoadScene("MainGame");
    }
}
