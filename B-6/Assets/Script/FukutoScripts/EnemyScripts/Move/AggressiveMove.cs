using System;
using UnityEngine;

/// <summary>
/// アグレッシブムーブ
/// 
/// 攻撃的な敵の移動処理を行うクラス
/// </summary>
public class AggressiveMove : BaseEnemyMove
{
    #region Config
    [SerializeField] private float searchDistance = 10f; // 探索距離
    #endregion

    #region State
    private Transform targetPlayer; // ターゲットのプレイヤー
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
        // ターゲットの切り替え
        ConvertTarget();

        // 接近判定
        CheckAttaced();

        // 移動停止フラグがtrueの場合止める
        if (stopMovement) return;

        // 壁の確認
        CheckIsWall();

        //CheckIsTower();

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
    }

    /// @brief ターゲットを切り替える処理を行う関数
    protected void ConvertTarget()
    {
        // プレイヤーを探す
        targetPlayer = FindPlayerWithinRange(searchDistance);

        if (targetPlayer != null)
        {
            currentTarget = targetPlayer;
            // プレイヤーのサイズ取得
            PlayerSize playerSize = targetPlayer.GetComponent<PlayerSize>();
            targetSize = playerSize.GetPlayerSize();
        }
        else
        {
            currentTarget = targetTower;
            ResetHitFlag();
            // タワーのサイズを取得
            TowerSize towerSize = targetTower.GetComponent<TowerSize>();
            targetSize = towerSize.GetTowerSize();
        }

        if(currentTarget == null)
        {
            return;
        }
    }


    ///// @brief プレイヤーと敵の間のタワーを確認する関数
    //protected void CheckIsTower()
    //{
    //    //Wallのレイヤーを取得
    //    int wallLayerMask = LayerMask.GetMask("Tower");

    //    // 敵とタワーの直線上の間にWallのレイヤーオブジェクトがあるかをチェック
    //    RaycastHit2D hit = Physics2D.Linecast(transform.position, currentTarget.position, wallLayerMask);

    //    // もし壁に遮られていたら、横に移動
    //    if (hit.collider != null)
    //    {
    //        if (!obstructedWall)
    //        {
    //            DecideAvoidVelocity(hit.point);
    //        }
    //        transform.position = transform.position + (avoidVelocity * Time.deltaTime);
    //        followSpeed = 0f;
    //    }
    //    else
    //    {
    //        obstructedWall = false;
    //        followSpeed = settingSpeed;
    //    }
    //}

    /// @brief ヒットフラグをリセットする関数
    protected virtual void ResetHitFlag(){ }

    /// @brief プレイヤーを探索してトランスフォームを返す関数
    protected Transform FindPlayerWithinRange(float range)
    {
        // プレイヤーのオブジェクトの参照を取得
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Transform p_transform = null;

        // プレイヤーが見つからない場合はnullを返す
        if (player == null) return null;

        // 敵とプレイヤーの距離を算出
        float dist = Vector2.Distance(transform.position, player.transform.position);

        // 距離が索敵範囲よりも小さい場合
        if (dist < range)
        {
            // プレイヤーのトランスフォームを格納
            p_transform = player.transform;
        }

        // トランスフォームを返す
        return p_transform;
    }

    /// @brief ターゲットのプレイヤーを設定する関数
    public void SetTargetPlayer(Transform player)
    {
        targetPlayer = player;
    }
}
