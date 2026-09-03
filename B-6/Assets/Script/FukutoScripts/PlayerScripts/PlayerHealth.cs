using UnityEngine;

/// <summary>
/// プレイヤーヘルス
/// 
/// プレイヤーの体力クラス
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    #region Config
    private int myHp = 5; // HP
    #endregion

    #region State
    [SerializeField] private PlayerProgressData playerProgressData; // プレイヤーのデータ
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // データからHPを取得
        myHp = playerProgressData.hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// @brief 被ダメージ処理を行う関数
    public void ReceiveDamage(int dmg)
    {
        myHp -= dmg;
    }

    /// @brief 生死を判定するフラグ
    public bool IsAlive()
    {
        // hpが0だったらfalse
        if (myHp <= 0)
            return false;
        else
            return true;
    }
}
