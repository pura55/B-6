using UnityEngine;

/// <summary>
/// ドロップExp
/// 
/// 経験値をドロップ（生成）するクラス
/// </summary>
public class DropExp : MonoBehaviour
{
    #region State
    [SerializeField] private GameObject expFromDrop; // ドロップする経験値のオブジェクト
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    
    /// @brief 経験値を生成する関数
    public void EnemyDropExp()
    {
        Debug.Log("経験値ドロップ！");
        // オブジェクト生成
        GameObject dropedExp = Instantiate(expFromDrop, transform.position, Quaternion.identity);
    }

}
