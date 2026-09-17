using UnityEngine;

/// <summary>
/// ウェポントリガーマネージャー
/// 
/// 武器の当たり判定のトリガーを管理するスクリプト
/// </summary>
public class WeaponTriggerManager : HitTriggerManager
{
    #region Config
    private WeaponAnimation weaponAnimation;
    #endregion

    void Start()
    {
        // コンポーネントから取得
        hitTrigger = GetComponent<Collider2D>();

        weaponAnimation = GetComponent<WeaponAnimation>();

        // 攻撃力を取得
        SetStatSkill();
    }

    void Update()
    {
        // トリガーの状態を更新
        SwitchTrigger();
    }

    private void SwitchTrigger()
    {
        if (isActiveTrigger)
        {
            hitTrigger.enabled = true;
        }
        else
        {
            hitTrigger.enabled = false;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("トリガーON");
        // プレイヤーが判定内に入った場合
        if (collision.CompareTag("Player"))
        {
            // プレイヤーの体力の参照を取得
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            // プレイヤーのHPを減らす
            playerHealth.ReceiveDamage(statAtk);
        }

        if (collision.CompareTag("Tower"))
        {
            Debug.Log("トリガーON");
            // プレイヤーの体力の参照を取得
            TowerHealth towerHealth = collision.GetComponent<TowerHealth>();

            // プレイヤーのHPを減らす
            towerHealth.TakeDamage(statAtk);
        }
    }

    /// @brief スキルの攻撃力を設定する関数
    private void SetStatSkill()
    {
        // 親のコンポーネントから取得
        WeaponSkill weaponSkill = GetComponentInParent<WeaponSkill>();
        statAtk = weaponSkill.GetStatSkill();
    }
}
