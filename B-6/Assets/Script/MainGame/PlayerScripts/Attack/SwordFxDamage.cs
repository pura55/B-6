using UnityEngine;

/// <summary>
/// ソードFXダメージ
/// 
/// スキル使用時の剣のエフェクトダメージを与えるクラス
/// </summary>
public class SwordFxDamage : MonoBehaviour
{
    #region Config
    // プレイヤーのデータ
    [Header("DATA")]
    [SerializeField] private PlayerProgressData playerProgressData;
    #endregion

    #region State
    private int effectDamage = 0; // エフェクトのダメージ
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitValue();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(CheckEffectDamage(collision))
        {
            DealDamage(collision);
            return;
        }
    }

    /// @brief 初期化関数
    private void InitValue()
    {
        effectDamage = playerProgressData.atkDmg;
        return;
    }

    /// @brief ダメージを与えるか判定する関数
    private bool CheckEffectDamage(Collider2D collision)
    {
        if(collision.transform.CompareTag("Enemy"))
        {
            return true;
        }

        return false;
    }

    /// @brief ダメージを与える関数
    private void DealDamage(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.transform.GetComponent<EnemyHealth>();

        if(enemyHealth != null)
        {
            enemyHealth.ReceiveDamage(effectDamage);
            return;
        }
    }
}
