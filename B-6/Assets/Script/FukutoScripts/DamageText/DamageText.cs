using UnityEngine;
using TMPro;

/// <summary>
/// ダメージテキスト
/// 
/// ダメージテキストの処理を管理します
/// </summary>
public class DamageText : APooledObject
{
    #region Config
    [SerializeField] private float moveSpeed = 1f; // 移動速度
    [SerializeField] private float lifeTime = 1f; // ライフタイム
    #endregion

    #region State]
    private float currentTime = 0f; // タイマー
    private RectTransform rectTransform;  // ダメージのトランスフォーム
    private TextMeshPro textMeshPro; // ダメージのテキスト
    #endregion


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = lifeTime;
        rectTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        DecreaseTime();
    }

    /// @brief 時間減少を行う関数
    private void DecreaseTime()
    {
        currentTime -= Time.captureDeltaTime;

        // ライフタイムが終了したら
        if( currentTime < 0f )
        {
            // プールに戻す
            Release();
        }
    }

    /// @brief 移動処理を行う関数
    private void Movement()
    {
        float amount = moveSpeed * Time.deltaTime;
        rectTransform.position += new Vector3(0f, amount, 0f);
    }

    /// @brief ダメージを設定する関数
    public void SetDamage(int damage)
    {
        textMeshPro.text = damage.ToString();
    }
}
