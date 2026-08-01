using UnityEditor.Animations;
using UnityEngine;

public class IncludeMovementAnimation: BaseEnemySpriteSelector
{
    #region Config
    protected NomalMove nomalMove;
    #endregion

    void Start()
    {
        InitValue();
    }
    void Update()
    {
        ManageRenderer();
    }

    /// @brief 描画を管理する関数
    protected override void ManageRenderer()
    {
        switch (animationState)
        {
            case AnimationState.idle:
                IdleAnimation();
                animationState = AnimationState.move;
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
        FlipSprite(nomalMove.GetMovementPerFrame());
    }

    /// @brief 移動アニメーション
    protected void MoveAnimation()
    {
        ManageFrame(moveSpriteElements);
        spriteRenderer.sprite = moveSprites[spriteIndex];
    }

    /// @brief 初期化関数
    protected override void InitValue()
    {
        SetBaseSprites();
        SetMoveSprite();
        SetSpriteElements();
        SetMoveElements();
        spriteRenderer = GetComponent<SpriteRenderer>();
        nomalMove = GetComponent<NomalMove>();
    }

    /// @brief 移動スプライトを設定する関数
    protected void SetMoveSprite()
    {
        // 移動スプライトを設定
        moveSprites = enemySpriteData.GetSprite(enemyId, moveSpriteName);
    }

    protected void SetMoveElements()
    {
        moveSpriteElements = moveSprites.Length;
    }
}
