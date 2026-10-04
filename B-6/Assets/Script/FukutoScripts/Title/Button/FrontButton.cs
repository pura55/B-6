using UnityEngine;

/// <summary>
/// フロントボタン
/// 
/// 説明スライドを前に戻すボタン
/// </summary>
public class FrontButton : ButtonBase
{
    [SerializeField] private ExplanationManager explanationManager; // ゲーム説明管理マネージャー
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public override void OnClick()
    {
        base.OnClick();
        explanationManager.BackExplanation();
    }
}
