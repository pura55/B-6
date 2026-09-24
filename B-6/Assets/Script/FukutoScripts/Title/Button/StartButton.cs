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
    [SerializeField] private SelectedExclamation selectExclamation;
    #endregion

    public override void OnClick()
    {
        if (selectAnimation.GetCharacterID() == 4 && !selectCharacterID.canSelected)
        {
            selectExclamation.SetAlpha();
            return;
        }

        // idを設定
        selectCharacterID.SetSelectID(selectAnimation.GetCharacterID());

        // シーン遷移
        SceneManager.LoadScene("MainGame");
        return;
    }
}
