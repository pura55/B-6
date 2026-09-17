using UnityEngine;

/// <summary>
/// ノーマルアニメーション
/// 
/// 通常のアニメーションスクリプト
/// </summary>
public class NomalAnimation : BaseEnemySpriteSelector
{
    void Start()
    {
        InitValue();
    }
    void Update()
    {
        ManageDrawing();
    }

    /// @brief 描画を管理する関数
    protected override void ManageDrawing()
    {
        switch (animationState)
        {
            case AnimationState.idle:
                IdleAnimation();
                break;
            case AnimationState.move:
                IdleAnimation();
                break;
            case AnimationState.attack:
                AttackAnimation();
                break;
            case AnimationState.hit:
                HitAnimation();
                break;
            case AnimationState.death:
                DeathAnimation();
                break;
        }
        SelectFripSprite();
    }

    /// @brief 初期化関数
    protected override void InitValue()
    {
        SetBaseSprites();
        SetSpriteElements();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetMovementScript();
    }
}
