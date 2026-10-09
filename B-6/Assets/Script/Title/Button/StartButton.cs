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
    [SerializeField] private GameManager gameManager; // ゲームマネージャー
    #endregion

    #region State
    private int rockCharacterID = 4; // ロックがかかっているキャラクターのＩＤ
    #endregion

    public override void OnClick()
    {
        base.OnClick();

        if (selectAnimation.GetCharacterID() == rockCharacterID && !selectCharacterID.canSelected)
        {
            selectExclamation.SetAlpha();
            return;
        }

        // idを設定
        selectCharacterID.SetSelectID(selectAnimation.GetCharacterID());

        // シーン遷移
        gameManager.ChangeMainGame();
        return;
    }
}
