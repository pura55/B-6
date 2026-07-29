using UnityEngine;

/// <summary>
/// ベースエネミーマネージャー
/// 
/// 敵を管理処理を行う基底クラス
/// </summary>
public class BaseEnemyManager : MonoBehaviour
{
    protected enum EnemyState
    {
        Idle,      // 待機
        Move,      // 移動
        Attack,    // 攻撃
        Hit,       // 被攻撃
        Dead       // 死亡
    }

    protected EnemyState enemyState = EnemyState.Idle; // 敵の状態

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
