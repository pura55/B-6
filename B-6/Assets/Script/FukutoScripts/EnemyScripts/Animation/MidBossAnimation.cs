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
    [Header("CHARGE")]
    [SerializeField] private bool isCharge = false; // チャージフラグ（スキルでチャージをするかどうか）
    [SerializeField] private int stopSpriteIndex = 1; // 停止するスプライトの指数
    [SerializeField] private float chargeTime = 3f; // 溜めの時間（目標）
    #endregion

    #region State
    protected Sprite[] skillSprites;     // スキルスプライト
    protected int skillSpriteElements = 0; // スキルスプライトの要素数
    protected string skillSpriteName = "SKILL";   // スキルスプライト名

    private float chargeCount = 0f; // 溜めのカウント
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
