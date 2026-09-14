using UnityEngine;

/// <summary>
/// セレクトボタン
/// 
/// キャラクターを選択するボタンクラス
/// </summary>
public class SelectButton : ButtonBase
{
    #region Config
    [SerializeField] private bool toNext = false; // 次へ進めるかどうかのフラグ
    [SerializeField] private SelectAnimation selectAnimation; // 選択アニメーションクラス
    #endregion

    public override void OnClick()
    {
        if(!toNext)
        {
            selectAnimation.BackToSprites();
        }
        else
        {
            selectAnimation.NextToSprites();
        }
    }
}
