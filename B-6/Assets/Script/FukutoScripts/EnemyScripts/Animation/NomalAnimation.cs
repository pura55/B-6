using UnityEngine;

public class NomalAnimation : BaseEnemySpriteSelector
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
    protected override void ManageRenderer()
    {
        switch (animationState)
        {
            case AnimationState.idle:
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
        FlipSprite(nomalMove.GetMovementPerFrame());
    }

    protected override void InitValue()
    {
        SetBaseSprites();
        SetSpriteElements();
        spriteRenderer = GetComponent<SpriteRenderer>();
        nomalMove = GetComponent<NomalMove>();
    }
}
