using System;
using UnityEngine;

/// <summary>
/// アグレッシブムーブ
/// 
/// 攻撃的な敵の移動処理を行うクラス
/// </summary>
public class AggressiveMove : BaseEnemyMove
{
    #region State
    [SerializeField] private Transform targetPlayer; // ターゲットのプレイヤー
    #endregion

    void Start()
    {
        InitValue();
    }

    void Update()
    {
        ManageMoving();
    }

    /// @brief 移動を管理する関数
    protected override void ManageMoving()
    {
        ConvertTarget();

        switch (moveState)
        {
            case EnemyMoveState.idle:
                CheckAttaced();
                break;
            case EnemyMoveState.attaching:
                if (!isAttached)
                {
                    AttachTower();
                    CheckAttaced();
                    CheckIsWall();
                }
                break;
        }
    }

    /// @brief 変数を初期化する関数
    protected override void InitValue()
    {
        moveState = EnemyMoveState.attaching;
    }

    /// @brief ターゲットを切り替える処理を行う関数
    private void ConvertTarget()
    {
        if (targetPlayer != null)
        {
            currentTarget = targetPlayer;
        }
        else
        {
            currentTarget = targetTower;
        }
    }

    /// @brief ターゲットのプレイヤーを設定する関数
    public void SetTargetPlayer(Transform player)
    {
        targetPlayer = player;
    }
}
