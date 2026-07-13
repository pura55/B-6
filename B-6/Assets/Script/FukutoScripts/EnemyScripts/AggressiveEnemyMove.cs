using System;
using UnityEngine;

public class AggressiveEnemyMove : MonoBehaviour
{
    enum AggressiveEnemyState
    {
        idle,     // idle
        attaching // 接近  
    }

    #region Config
    private float followSpeed = 1f; // 追従速度
    #endregion

    #region State
    [SerializeField] private Transform targetTower;  // ターゲットのタワー
    [SerializeField] private Transform targetPlayer; // ターゲットのプレイヤー
    private Transform currentTarget;                 // 現在のターゲット
    private AggressiveEnemyState moveState = AggressiveEnemyState.attaching; // 敵の移動ステート
    private bool isAttached = false;                             // 近づいているかどうかのフラグ 
    private Vector3 avoidVelocity = new Vector3(0f, 0f, 0f);     // 避ける速度
    private bool hitWall = false;
    #endregion

    void Start()
    {
    }

    void Update()
    {
        ConvertTarget();

        switch (moveState)
        {
            case AggressiveEnemyState.idle:
                CheckAttaced();
                break;
            case AggressiveEnemyState.attaching:
                if (!isAttached)
                {
                    AttachTower();
                    CheckAttaced();
                    CheckIsWall();
                }
                break;
        }
    }

    // タワーに近づく処理を行う関数
    private void AttachTower()
    {
        // 割合計算で座標を移動させるため減速処理が入る
        // 使用するかもしれないのでコメントアウトしておく
        //transform.position = Vector3.Lerp(
        //                transform.position,
        //                targetTower.position,
        //                followSpeed * Time.deltaTime
        //);

        // 一定の速さで移動させる
        transform.position = Vector3.MoveTowards(
            transform.position,
            currentTarget.position,
            followSpeed * Time.deltaTime
        );
    }

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

    // 一定距離に近づいたか確認する関数
    private void CheckAttaced()
    {
        if (Vector2.Distance(transform.position, currentTarget.position) < 1f)
        {
            isAttached = true;
            moveState = AggressiveEnemyState.idle;
        }
        else 
        {
            isAttached = false;
            moveState = AggressiveEnemyState.attaching;
        }
    }

    // 壁があるか判定する関数
    private void CheckIsWall()
    {
        //Wallのレイヤーを取得
        int wallLayerMask = LayerMask.GetMask("Wall");

        // 敵とタワーの直線上の間にWallのレイヤーオブジェクトがあるかをチェック
        RaycastHit2D hit = Physics2D.Linecast(transform.position, currentTarget.position, wallLayerMask);

        // もし壁に遮られていたら、横に移動
        if (hit.collider != null)
        {
            if (!hitWall)
            {
                DecideAvoidVelocity(hit.point);
            }
            transform.position = transform.position + (avoidVelocity * Time.deltaTime);
            followSpeed = 0f;
        }
        else
        {
            hitWall = false;
            followSpeed = 1f;
        }
    }

    // 座標によってよける向きや速度を決める関数
    // 座標によってよける向きや速度を決める関数
    private void DecideAvoidVelocity(Vector3 point)
    {
        // 距離の差分
        Vector3 diffDistance = point - transform.position;
        Vector3 unsignVector3 = new Vector3(0f, 0f, 0f);
        // XY軸の避ける向きと速度
        float avoidVelocityX = 0f;
        float avoidVelocityY = 0f;

        // xの差分を符号なしに変換
        if (diffDistance.x < 0f)
            unsignVector3.x = -diffDistance.x;
        else
            unsignVector3.x = diffDistance.x;

        // yの差分を符号なしに変換
        if (diffDistance.y < 0f)
            unsignVector3.y = -diffDistance.y;
        else
            unsignVector3.y = diffDistance.y;

        // 差分の少ない方の速度を一定の値に設定
        if (unsignVector3.x < unsignVector3.y)
        {
            if (diffDistance.x < 0f)
                avoidVelocityX = -1f;
            else if (diffDistance.x >= 0f)
                avoidVelocityX = 1f;
        }
        else
        {
            if (diffDistance.y < 0f)
                avoidVelocityY = -1f;
            else if (diffDistance.y >= 0f)
                avoidVelocityY = 1f;
        }

        // 決めた値を代入
        avoidVelocity = new Vector3(avoidVelocityX, avoidVelocityY, 0f);

        hitWall = true;
    }

    // ターゲットのタワーを設定する関数
    public void SetTargetTower(Transform tower)
    {
        targetTower = tower;
    }

    public void SetTargetPlayer(Transform player)
    {
        targetPlayer = player;
    }
}
