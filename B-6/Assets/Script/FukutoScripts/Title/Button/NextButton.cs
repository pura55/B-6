using UnityEngine;

/// <summary>
/// ネクストボタン
/// 
/// 説明スライドを進めるボタン
/// </summary>
public class NextButton : ButtonBase
{
    [SerializeField] private ExplanationManager explanationManager; // ゲーム説明管理マネージャー
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public override void OnClick()
    {
        base.OnClick();
        explanationManager.NextExplanation();
    }
}
