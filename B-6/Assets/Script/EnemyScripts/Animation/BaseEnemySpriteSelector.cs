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
    [SerializeField] protected int enemyId = 0; // 敵のID
    [SerializeField] protected bool reverseFlip = false; // スプライトのフリップを反転するフラグ
    [Header("MOVE")]
    [SerializeField] protected bool onNomalMove = false; // 基本移動が付いているかどうかのフラグ
    [SerializeField] protected bool onAggressiveMove = false; // プレイヤー追跡の移動がついているかどうかのフラグ
    [SerializeField] protected bool onBossMove = false; // プレイヤー追跡の移動がついているかどうかのフラグ
    protected float timePerSprite = 0.1f; // 毎スプライトごとの時間
    #endregion

    #region State
    protected AnimationState animationState = AnimationState.idle;
    protected int spriteIndex = 0; // スプライトの指数
    protected float currentTime = 0; // 現在のアニメーション時間
    protected bool isFinishedEvent = true; // イベントアニメーションの終了フラグ
    protected bool isFinishedDeath = false; // 死亡アニメーションの終了フラグ

    protected int idleSpriteElements = 0; // 待機スプライトの要素数
    protected int attackSpriteElements = 0; // 攻撃スプライトの要素数
    protected int hitSpriteElements = 0; // 被ダメージスプライトの要素数
    protected int deathSpriteElements = 0; // 死亡スプライトの要素数
    protected int moveSpriteElements = 0; // 移動スプライトの要素数

    protected string idleSpriteName = "IDLE";   // 待機スプライト名
    protected string attackSpriteName = "ATTACK"; // 攻撃スプライト名
    protected string hitSpriteName = "HIT";    // 被ダメージスプライト名
    protected string deathSpriteName = "DEATH";  // 死亡スプライト名
    protected string moveSpriteName = "MOVE";  // 移動スプライト名

    protected Sprite[] idleSprites;     // 待機スプライト
    protected Sprite[] attackSprites;   // 攻撃スプライト
    protected Sprite[] hitSprites;      // 被ダメージスプライト
    protected Sprite[] deathSprites;    // 死亡スプライト
    protected Sprite[] moveSprites;     // 移動スプライト
    protected SpriteRenderer spriteRenderer; // スプライトレンダラー
    protected NomalMove nomalMove; // 通常の移動スクリプト
    protected AggressiveMove aggressiveMove; // プレイヤーを追尾するスクリプト
    protected BossMove bossMove; // ボスの移動
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
        ManageEventFrame(hitSpriteElements);
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
        // 現在の時間がスプライトごとの時間よりも小さい場合
        if (currentTime < timePerSprite)
        {
            currentTime += Time.deltaTime; // 時間を進める
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
            currentTime = 0f;
        }
    }

    /// @brief 攻撃、被ダメージ、スキルのフレーム管理を行う関数
    protected void ManageEventFrame(int elements)
    {
        // 現在の時間がスプライトごとの時間よりも小さい場合
        if (currentTime < timePerSprite)
        {
            currentTime += Time.deltaTime; // 時間を進める
        }
        else
        {
            // 指数 + 1 が要素以上だったら指数を戻す
            if (elements <= (spriteIndex + 1))
            {
                spriteIndex = 0;
                currentTime = 0;
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
            currentTime = 0f;
        }
    }

    /// @brief死亡時のフレーム管理を行う関数
    protected void ManageDeathFrame(int elements)
    {
        // フレームがFPSよりも小さい場合
        if (currentTime < timePerSprite)
        {
            currentTime += Time.deltaTime; // 時間を進める
        }
        else
        {
            // 指数 + 1 が要素以上だったら指数を戻す
            if (elements <= (spriteIndex + 1))
            {
                // 死亡時はreturn
                isFinishedDeath = true;
                return;
            }
            else
            {
                // スプライト指数を進める
                spriteIndex++;
            }

            // フレームをリセット
            currentTime = 0f;
        }
    }

    /// @briefスプライトのフリップ処理を行う関数
    protected void FlipSprite(Vector3 amount)
    {
        if(!reverseFlip)
        {
            if (amount.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            if (amount.x < 0)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
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

    /// @brief 移動スクリプトを設定する関数
    protected void SetMovementScript()
    {
        // NomalMoveのコンポーネントの取得
        if (onNomalMove) nomalMove = GetComponent<NomalMove>();

        // AggressiveMoveのコンポーネントの取得
        else if (onAggressiveMove) aggressiveMove = GetComponent<AggressiveMove>();

        else if (onBossMove) bossMove = GetComponent<BossMove>();
    }

    /// @brief スプライトのフリップ処理を選択する関数
    protected void SelectFripSprite()
    {
        // NomalMoveのコンポーネントの取得
        if (onNomalMove) FlipSprite(nomalMove.GetMovementPerFrame());

        // AggressiveMoveのコンポーネントの取得
        else if (onAggressiveMove) FlipSprite(aggressiveMove.GetMovementPerFrame());

        else if (onBossMove) FlipSprite(bossMove.GetMovementPerFrame());
    }

    /// @brief フレームとスプライト指数をリセットする関数
    public void ResetFrameAndIndex()
    {
        currentTime = 0;
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

    /// @brief 描画状態を被ダメージに設定する関数 
    public void SetHit()
    {
        animationState = AnimationState.hit;
    }

    /// @brief 描画状態を死亡に設定する関数 
    public void SetDeath()
    {
        animationState = AnimationState.death;
    }

    /// @brief イベントアニメーションの終了フラグを返す関数
    public bool GetFinishedEvent()
    {
        return isFinishedEvent;
    }

    /// @brief 死亡アニメーションの終了フラグを返す関数
    public bool GetFinishedDeath()
    {
        return isFinishedDeath;
    }

    /// @brief スプライトの指数を返す関数
    public int GetSpriteIndex()
    {
        return spriteIndex;
    }
}
