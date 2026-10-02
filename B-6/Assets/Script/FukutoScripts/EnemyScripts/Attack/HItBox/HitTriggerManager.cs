using UnityEngine;

/// <summary>
/// ヒットトリガーマネージャー
/// 
/// 当たり判定のトリガーを管理するスクリプト
/// </summary>
public class HitTriggerManager : MonoBehaviour
{
    #region Config
    protected int statAtk;
    #endregion

    #region State
    protected bool isActiveTrigger = false; // 当たり判定のアクティブフラグ（true: アクティブ, false: 非アクティブ)
    protected Collider2D hitTrigger; // 当たり判定のトリガー
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // コンポーネントから取得
        hitTrigger = GetComponent<Collider2D>();

        // 攻撃力を取得
        SetStatAttack();
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
            playerHealth.ReceiveDamage(statAtk);
        }

        if (collision.CompareTag("Tower"))
        {
            // プレイヤーの体力の参照を取得
            TowerHealth towerHealth = collision.GetComponent<TowerHealth>();

            // プレイヤーのHPを減らす
            towerHealth.TakeDamage(statAtk);
        }
    }

    /// @brief 当たり判定のON・OFFをスイッチする関数
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

    /// @brief 当たり判定のON・OFFフラグを設定する関数
    public void SetHitTrigger(bool trigger)
    {
        isActiveTrigger = trigger;
    }

    /// @biref 攻撃力を設定する関数
    private void SetStatAttack()
    {
        // 親のコンポーネントから取得
        ShortAttack shortAttack = GetComponentInParent<ShortAttack>();
        statAtk = shortAttack.GetStatAttack();
    }
}
