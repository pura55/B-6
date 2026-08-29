using UnityEngine;

/// <summary>
/// ウェポンアニメーション
/// 
/// 武器専用アニメーションクラス
/// </summary>
public class WeaponAnimation : MonoBehaviour
{
    protected enum AnimationState
    {
        idle,   // 待機
        act     // アニメーション実行
    }

    #region Config
    [SerializeField] protected int enemyId = 0; // 敵のID
    protected float timePerSprite = 0.1f; // 毎スプライトごとの時間
    #endregion

    #region State
    protected AnimationState animationState = AnimationState.idle;
    protected int spriteIndex = 0; // スプライトの指数
    protected float currentTime = 0; // 現在のアニメーション時間
    protected bool isPermissionAct = false; // アニメーション実行フラグ
    protected bool isFinishedAnimation = true; // アニメーションの終了フラグ
    protected int weaponSpriteElements = 0; // 武器スプライトの要素数
    protected string weaponSpriteName = "WEAPON";  // 武器スプライト名
    protected Sprite[] weaponSprites;     // 武器スプライト
    protected SpriteRenderer spriteRenderer; // スプライトレンダラー
    [SerializeField] protected EnemySpriteData enemySpriteData; // スプライトデータ
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitValue();
    }

    // Update is called once per frame
    void Update()
    {
        ManageDrawing();
    }

    private void ManageDrawing()
    {
        switch (animationState)
        {
            case AnimationState.idle:
                if(isPermissionAct)
                {
                    animationState = AnimationState.act;
                }
                break;
            case AnimationState.act:
                ActAnimation();
                break;
        }
    }

    /// @brief 変数を初期化する関数
    private void InitValue()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetWeaponSprite();
        SetSpriteElements();
    }

    /// @brief 武器アニメーション
    protected void ActAnimation()
    {
        ManageSprite(weaponSpriteElements);
        spriteRenderer.sprite = weaponSprites[spriteIndex];
    }

    /// @brief スプライトを管理する関数
    protected void ManageSprite(int elements)
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
                isFinishedAnimation = true;
                isPermissionAct = false;
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

    /// @brief 基本スプライトを設定する関数
    protected void SetWeaponSprite()
    {
        weaponSprites = enemySpriteData.GetSprite(enemyId, weaponSpriteName);
    }

    /// @brief スプライトの要素数を取得する関数
    protected void SetSpriteElements()
    {
        weaponSpriteElements = weaponSprites.Length;
    }

    /// @brief アニメーション実行フラグを設定する関数
    public void SetPermissionAct(bool act)
    {
        isPermissionAct = act;
    }
}
