using UnityEngine;

/// <summary>
/// ミッドボスアニメーション
/// 
/// 中ボスのアニメーションクラス
/// </summary>
public class MidBossAnimation : IncludeMovementAnimation
{
    #region State
    protected Sprite[] skillSprites;     // スキルスプライト
    protected int skillSpriteElements = 0; // スキルスプライトの要素数
    protected string skillSpriteName = "SKILL";   // スキルスプライト名
    #endregion

    void Start()
    {
        InitValue();
    }
    void Update()
    {
        ManageDrawing();
    }

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
            case AnimationState.skill:
                SkillAnimation();
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
        // スプライト
        SetBaseSprites();
        SetMoveSprite();
        SetSkillSprite();

        // 要素数
        SetSpriteElements();
        SetMoveElements();
        SetSkillElements();

        spriteRenderer = GetComponent<SpriteRenderer>();
        SetMovementScript();
    }

    /// @brief 移動アニメーション
    protected void SkillAnimation()
    {
        ManageEventFrame(skillSpriteElements);
        spriteRenderer.sprite = skillSprites[spriteIndex];
    }

    /// @brief 移動スプライトを設定する関数
    protected void SetSkillSprite()
    {
        // 移動スプライトを設定
        skillSprites = enemySpriteData.GetSprite(enemyId, skillSpriteName);
    }

    /// @brief 移動スプライトの要素を設定する関数
    protected void SetSkillElements()
    {
        skillSpriteElements = skillSprites.Length;
    }
}
