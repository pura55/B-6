using UnityEngine;

/// <summary>
/// ベースエネミースプライトセレクター
/// 
/// 敵の移動処理を行う基底クラス
/// </summary>
public abstract class BaseEnemySpriteSelector : MonoBehaviour
{
    #region State
    protected SpriteRenderer spriteRenderer; // スプライトレンダラー
    protected string textureBasePass = "Enemies/"; // 基本テクスチャーパス
    protected string idleTexturePass;   // 待機テクスチャーパス
    protected string attackTexturePass; // 攻撃テクスチャーパス
    protected string hitTexturePass;    // 被ダメージテクスチャーパス
    protected string deathTexturePass;  // 死亡テクスチャーパス
    protected Sprite[] idleSprites;     // 待機スプライト
    protected Sprite[] attackSprites;   // 攻撃スプライト
    protected Sprite[] hitSprites;      // 被ダメージスプライト
    protected Sprite[] deathSprites;    // 死亡スプライト
    #endregion

    /// @brief 移動を管理する関数 (子で上書き） 
    protected abstract void ManageRenderer();

    /// @brief 変数を初期化する関数（子で上書き）
    protected abstract void InitValue();

    /// @brief 待機アニメーション
    protected void IdleAnimation(){}

    /// @brief 待機アニメーション
    protected void AttackAnimation() { }

    /// @brief 待機アニメーション
    protected void HitAnimation() { }
}
