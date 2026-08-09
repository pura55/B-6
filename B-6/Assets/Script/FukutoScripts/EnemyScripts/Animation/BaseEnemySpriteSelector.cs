using UnityEngine;

/// <summary>
/// ベースエネミースプライトセレクター
/// 
/// 敵の移動処理を行う基底クラス
/// </summary>
public abstract class BaseEnemySpriteSelector : MonoBehaviour
{
    // アニメーションの状態
    protected enum AnimationState
    { 
        idle,   // 待機
        attack, // 攻撃
        hit,    // 被ダメージ
        death,  // 死亡
        move,   // 移動
        skill   // スキル
    }
    #region Config
    [SerializeField]protected int enemyId = 0; // 敵のID
    protected int framePerSprite = 60; // 毎スプライトごとのフレーム数
    #endregion

    #region State
    protected AnimationState animationState = AnimationState.idle;
    protected int spriteIndex = 0; // スプライトの指数
    protected int currentFrame = 0; // 現在のフレーム
    protected bool isFinishedEvent = true; // イベントアニメーションの終了フラグ

    protected int idleSpriteElements = 0; // 待機スプライトの要素数
    protected int attackSpriteElements = 0; // 攻撃スプライトの要素数
    protected int hitSpriteElements = 0; // 被ダメージスプライトの要素数
    protected int deathSpriteElements = 0; // 死亡スプライトの要素数
    protected int moveSpriteElements = 0; // 移動スプライトの要素数

    protected string idleSpriteName = "IDLE";   // 待機スプライト名
    protected string attackSpriteName = "ATTACK"; // 攻撃スプライト名
    protected string hitSpriteName = "HIT";    // 被ダメージスプライト名
    protected string deathSpriteName = "DEATH";  // 死亡スプライト名
    protected string moveSpriteName = "MOVE";  // 死亡スプライト名

    protected Sprite[] idleSprites;     // 待機スプライト
    protected Sprite[] attackSprites;   // 攻撃スプライト
    protected Sprite[] hitSprites;      // 被ダメージスプライト
    protected Sprite[] deathSprites;    // 死亡スプライト
    protected Sprite[] moveSprites;     // 移動スプライト
    protected SpriteRenderer spriteRenderer; // スプライトレンダラー
    [SerializeField] protected EnemySpriteData enemySpriteData; // スプライトデータ
    #endregion

    /// @brief 描画を管理する関数 (子で上書き） 
    protected abstract void ManageDrawing();

    /// @brief 変数を初期化する関数（子で上書き）
    protected abstract void InitValue();

    /// @brief 待機アニメーション
    protected void IdleAnimation()
    {
        ManageFrame(idleSpriteElements);
        spriteRenderer.sprite = idleSprites[spriteIndex];
    }

    /// @brief 攻撃アニメーション
    protected void AttackAnimation() 
    {
        isFinishedEvent = false;
        ManageEventFrame(attackSpriteElements);
        spriteRenderer.sprite = attackSprites[spriteIndex];
    }

    /// @brief 被ダメージアニメーション
    protected void HitAnimation() 
    {
        isFinishedEvent = false;
        ManageFrame(hitSpriteElements);
        spriteRenderer.sprite = hitSprites[spriteIndex];
    }

    /// @brief 死亡アニメーション
    protected void DeathAnimation()
    {
        ManageDeathFrame(deathSpriteElements);
        spriteRenderer.sprite = deathSprites[spriteIndex];
    }

    /// @brief フレーム管理を行う関数
    protected void ManageFrame(int elements)
    {
        // フレームがFPSよりも小さい場合
        if (currentFrame < framePerSprite)
        {
            currentFrame++; // フレームを進める
        }
        else
        {

            // 指数 + 1 が要素以上だったら指数を戻す
            if (elements <= (spriteIndex + 1))
            {
                spriteIndex = 0;
            }
            else
            {
                // スプライト指数を進める
                spriteIndex++;
            }

            // フレームをリセット
            currentFrame = 0;
        }
    }

    /// @brief 攻撃、被ダメージ、スキルのフレーム管理を行う関数
    protected void ManageEventFrame(int elements)
    {
        // フレームがFPSよりも小さい場合
        if (currentFrame < framePerSprite)
        {
            currentFrame++; // フレームを進める
        }
        else
        {
            // 指数 + 1 が要素以上だったら指数を戻す
            if (elements <= (spriteIndex + 1))
            {
                spriteIndex = 0;
                currentFrame = 0;
                isFinishedEvent = true;
                animationState = AnimationState.idle;
                return;
            }
            else
            {
                // スプライト指数を進める
                spriteIndex++;
            }

            // フレームをリセット
            currentFrame = 0;
        }
    }

    /// @brief死亡時のフレーム管理を行う関数
    protected void ManageDeathFrame(int elements)
    {
        // フレームがFPSよりも小さい場合
        if (currentFrame < framePerSprite)
        {
            currentFrame++; // フレームを進める
        }
        else
        {
            // 指数 + 1 が要素以上だったら指数を戻す
            if (elements <= (spriteIndex + 1))
            {
                // 死亡時は指数を固定
                spriteIndex = elements - 1;
            }
            else
            {
                // スプライト指数を進める
                spriteIndex++;
            }

            // フレームをリセット
            currentFrame = 0;
        }
    }

    /// @briefスプライトのフリップ処理を行う関数
    protected void FlipSprite(Vector3 amount)
    {
        if(amount.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    /// @brief 基本スプライトを設定する関数
    protected void SetBaseSprites()
    {
        // 各スプライトを設定
        idleSprites = enemySpriteData.GetSprite(enemyId, idleSpriteName);
        attackSprites = enemySpriteData.GetSprite(enemyId, attackSpriteName);
        hitSprites = enemySpriteData.GetSprite(enemyId, hitSpriteName);
        deathSprites = enemySpriteData.GetSprite(enemyId, deathSpriteName);
    }

    /// @brief スプライトの要素数を取得する関数
    protected void SetSpriteElements()
    {
        idleSpriteElements = idleSprites.Length;
        attackSpriteElements = attackSprites.Length;
        hitSpriteElements = hitSprites.Length;
        deathSpriteElements = deathSprites.Length;
    }

    /// @brief フレームとスプライト指数をリセットする関数
    public void ResetFrameAndIndex()
    {
        currentFrame = 0;
        spriteIndex = 0;
    }

    /// @brief 描画状態を待機設定する関数 
    public void SetIdle()
    {
        animationState = AnimationState.idle;
    }

    /// @brief 描画状態を待機設定する関数 
    public void SetMove()
    {
        animationState = AnimationState.move;
    }

    /// @brief 描画状態を攻撃に設定する関数 
    public void SetAttack()
    {
        animationState = AnimationState.attack;
    }

    public void SetHit()
    {
        animationState = AnimationState.hit;
    }

    /// @brief イベントアニメーションがの終了フラグを返す関数
    public bool GetFinishedEvent()
    {
        return isFinishedEvent;
    }

    /// @brief スプライトの指数を返す関数
    public int GetSpriteIndex()
    {
        return spriteIndex;
    }
}
