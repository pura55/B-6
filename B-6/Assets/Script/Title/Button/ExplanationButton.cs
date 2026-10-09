using UnityEngine;

/// <summary>
/// エクスプラネーションボタン
/// 
/// ゲーム説明を開くボタン
/// </summary>
public class ExplanationButton : ButtonBase
{
    [SerializeField] private GameObject explanationScreen; // 遊び方スクリーン

    public override void OnClick()
    {
        base.OnClick();
        explanationScreen.SetActive(true);
    }
}
