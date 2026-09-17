using UnityEngine;
using UnityEngine.SceneManagement; // 追加

/// <summary>
/// スタートボタン
/// 
/// ゲーム開始ボタン
/// </summary>
public class StartButton : ButtonBase
{
    #region Config
    [SerializeField] private SelectAnimation selectAnimation;
    [SerializeField] private SelectCharacterID selectCharacterID;
    #endregion

    public override void OnClick()
    {
        // idを設定
        selectCharacterID.SetSelectID(selectAnimation.GetCharacterID());

        // シーン遷移
        SceneManager.LoadScene("MainGame");
    }
}
