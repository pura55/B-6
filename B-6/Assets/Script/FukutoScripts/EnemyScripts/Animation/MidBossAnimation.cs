using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

/// <summary>
/// ミッドボスアニメーション
/// 
/// 中ボスのアニメーションクラス
/// </summary>
public class MidBossAnimation : IncludeMovementAnimation
{
    #region Config
    /// @brief スキルのチャージの設定値
    [Header("CHARGE")]
    [SerializeField] private bool isCharge = false; // チャージフラグ（スキルでチャージをするかどうか）
    [SerializeField] private int stopSpriteIndex = 1; // 停止するスプライトの指数
    [SerializeField] private float chargeTime = 3f; // 溜めの時間（目標）
    [SerializeField] private float deathEffectTime = 2f; // 死亡時の演出時間

    /// @brief 波エフェクトの設定値
    [Header("WAVE EFFECT")]
    [SerializeField] private float loopSpeed = 75f; // ループ速度
    [SerializeField] private float waveSize = 0.01f; // 波の大きさ
    #endregion

    #region State
    protected Sprite[] skillSprites;     // スキルスプライト
    protected int skillSpriteElements = 0; // スキルスプライトの要素数
    protected string skillSpriteName = "SKILL";   // スキルスプライト名

    private float chargeCount = 0f; // 溜めのカウント

    private bool dethEffectFinished = false; // 死亡時エフェクトの終了フラグ
    private const int deathEffectSprite = 1; // 死亡時の演出を出すスプライト
    private float deathEffectCount = 0f; // 死亡時演出のカウント
    private float effectWave = 0f; // エフェクトの波
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
                if(isCharge)
                {
                    chargeCount = 0f;
                }
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
                if(spriteIndex == deathEffectSprite && dethEffectFinished)
                {
                    // 死亡エフェクトを処理
                    DeathEffect();
                }
                else
                {
                    DeathAnimation();
                }
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

        if(attackSprites == null && enemyId == 11)
        {
            Debug.Log("大ボスのデータが格納されていません");
        }
    }

    /// @brief 移動アニメーション
    protected void SkillAnimation()
    {
        if(!isCharge)
        {
            ManageEventFrame(skillSpriteElements);
        }
        else
        {
            ManageEventFrame(skillSpriteElements, stopSpriteIndex);
        }

        spriteRenderer.sprite = skillSprites[spriteIndex];
    }

    /// @brief イベント時のフレーム管理を行う関数 
    /// @param stopIndex 溜め攻撃を行うタイミングの指数
    protected void ManageEventFrame(int elements, int stop)
    {
        if(chargeTime < chargeCount)
        {
            ManageEventFrame(elements);
            
        }
        else
        {
            // 現在の時間がスプライトごとの時間よりも小さい場合
            if (currentTime < timePerSprite)
            {
                currentTime += Time.deltaTime; // 時間を進める
            }
            else
            {
                // 指数 + 1 がより大きかったら指数を戻す
                if (stop == spriteIndex)
                {
                    chargeCount += Time.deltaTime;
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
    }

    /// @brief 死亡時の演出を行う関数
    private void DeathEffect()
    {
        // 演出時間を超えたら
        if(deathEffectTime <= deathEffectCount)
        {
            dethEffectFinished = true;
            return;
        }
        else
        {
            // 揺らす演出をするためコサインを使用
            float loopTime = Time.time * loopSpeed;
            effectWave = Mathf.Cos(loopTime) * waveSize;

            transform.position = new Vector3(transform.position.x + effectWave, transform.position.y, transform.position.z);

            // 演出時間を進める
            deathEffectCount += Time.deltaTime;
            return;
        }
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

    /// @brief 描画状態をスキルに設定する関数 
    public void SetSkill()
    {
        animationState = AnimationState.skill;
    }
}
