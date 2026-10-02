using UnityEditor.Animations;
using UnityEngine;

/// <summary>
/// インクルードムーブメントアニメーション
/// 
/// 移動アニメーションが含まれているクラス
/// </summary>
public class IncludeMovementAnimation: BaseEnemySpriteSelector
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
                MoveAnimation();
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
        SetMoveSprite();
        SetSpriteElements();
        SetMoveElements();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetMovementScript();
    }

    /// @brief 移動アニメーション
    protected void MoveAnimation()
    {
        ManageFrame(moveSpriteElements);
        spriteRenderer.sprite = moveSprites[spriteIndex];
    }

    /// @brief 移動スプライトを設定する関数
    protected void SetMoveSprite()
    {
        // 移動スプライトを設定
        moveSprites = enemySpriteData.GetSprite(enemyId, moveSpriteName);
    }

    /// @brief 移動スプライトの要素を設定する関数
    protected void SetMoveElements()
    {
        moveSpriteElements = moveSprites.Length;
    }
}
