using System;
using UnityEngine;

/// <summary>
/// ノーマルムーブ
/// 
/// 通常の敵の移動処理を行うクラス
/// </summary>
public class NomalMove : BaseEnemyMove
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 初期化
        InitValue();
    }

    // Update is called once per frame
    void Update()
    {
        ManageMoving();
    }

    /// @brief 移動を管理する関数
    protected override void ManageMoving()
    {
        // 接近判定
        CheckAttaced();

        // 移動停止フラグがtrueの場合止める
        if (stopMovement) return;

        // 壁の確認
        CheckIsWall();

        // 接近していなければ接近処理
        if (!isAttached)
        {
            AttachTarget();
        }
    }

    /// @brief 変数を初期化する関数
    protected override void InitValue()
    {
        // 設定速度にデータから取得したスピードを格納
        settingSpeed = enemyProgressData.GetFloatStat(enemyID, speedStatName);
        followSpeed = settingSpeed;
        currentTarget = targetTower;

        // タワーのサイズを取得
        TowerSize towerSize = targetTower.GetComponent<TowerSize>();
        targetSize = towerSize.GetTowerSize();
    }
}
