using UnityEngine;

/// <summary>
/// ヒットトリガーマネージャー
/// 
/// 当たり判定のトリガーを管理するスクリプト
/// </summary>
public class HitTriggerManager : MonoBehaviour
{
    #region State
    private bool isActiveTrigger;
    private Collider2D hitTrigger;
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hitTrigger = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SwitchTrigger();
    }

    /// @ トリガーをスイッチする関数
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            // プレイヤーのHPを減らす
        }
    }

    /// @ トリガーを設定する関数
    public void SetHitTrigger(bool trigger)
    {
        isActiveTrigger = trigger;
    }
}
