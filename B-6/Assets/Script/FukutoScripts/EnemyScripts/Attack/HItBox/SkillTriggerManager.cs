using UnityEngine;

/// <summary>
/// ボストリガーマネージャー
/// 
/// ボスの当たり判定のトリガーを管理するスクリプト
/// </summary>
public class SkillTriggerManager : HitTriggerManager
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // コンポーネントから取得
        hitTrigger = GetComponent<Collider2D>();

        // 攻撃力を取得
        SetStatAttack();

        // スキルを取得
        SetStatSkill();
    }

    // Update is called once per frame
    void Update()
    {
        // トリガーの状態を更新
        SwitchTrigger();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // プレイヤーが判定内に入った場合
        if (collision.CompareTag("Player"))
        {
            // プレイヤーの体力の参照を取得
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            // プレイヤーのHPを減らす
            playerHealth.ReceiveDamage(statSkill);

            isSkill = false;
        }

        if (collision.CompareTag("Tower"))
        {
            // プレイヤーの体力の参照を取得
            TowerHealth towerHealth = collision.GetComponent<TowerHealth>();

            // プレイヤーのHPを減らす
            towerHealth.TakeDamage(statSkill);
            isSkill = false;
        }
    }

    /// @brief 当たり判定のON・OFFフラグを設定する関数
    /// @param isAttack 攻撃かどうかのフラグ（true: 攻撃, false: スキル)
    public void SetHitTrigger(bool trigger, bool isSkill)
    {

        isActiveTrigger = trigger;
    }

    /// @biref スキルを設定する関数
    protected void SetStatSkill()
    {
        // 親のコンポーネントから取得
        ShortSkill shortSkill = GetComponentInParent<ShortSkill>();
        statSkill = shortSkill.GetStatSkill();
    }
}
