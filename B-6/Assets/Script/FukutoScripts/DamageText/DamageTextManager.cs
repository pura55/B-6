using UnityEngine;

/// <summary>
/// ダメージUIファクトリー
/// </summary>
public class DamageTextFactory : IPooledObjectFactory
{
    #region Config
    [SerializeField] private readonly DamageText damageText; // ダメ―ジテキスト
    [SerializeField] private readonly Transform parent; // ダメージキャンバス
    #endregion

    /// @brief ファクトリーを生成する関数
    public DamageTextFactory(DamageText damageText, Transform parentTransform)
    {
        this.damageText = damageText;
        this.parent = parentTransform;
    }

    public APooledObject Create()
    {
        if (damageText == null)
        {
            Debug.LogError("プレハブが設定されていません。");
            return null;
        }

        DamageText instance = Object.Instantiate(damageText, parent);
        return instance;
    }
}

/// <summary>
/// ダメージテキストマネージャー
/// 
/// ダメージテキストを管理します
/// </summary>
public class DamageTextManager : MonoBehaviour
{
    [SerializeField] private DamageText damageText; // ダメージテキスト
    [SerializeField] private uint poolSize = 30; // プールサイズ

    private ObjectPool<DamageText> textPool; // オブジェクトのプール

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IPooledObjectFactory factory = new DamageTextFactory(damageText, this.transform);
        textPool = new ObjectPool<DamageText>(factory);
        textPool.PoolSetUp(poolSize); 
    }

    /// @brief ダメージを表示する関数
    public void ShowDamageText(Vector3 position, int damage)
    {
        // ダメージテキストをプールから取り出す
        DamageText damageText = textPool.GetFromPool() as DamageText;

        damageText.SetInfo(position, damage);
    }
}
