using UnityEngine;

/// <summary>
/// ベースエネミームーブ
/// 
/// 敵の移動処理を行う基底クラス
/// </summary>
public abstract class BaseEnemyMove : MonoBehaviour
{
    protected enum EnemyMoveState
    {
        idle,      // idle
        attaching  // 接近
    }

    #region Config
    protected float followSpeed = 1f; // 追従速度
    protected float settingSpeed = 1f; // 後から変えられる変数型定数
    [SerializeField] protected int enemyID; // 敵のID
    #endregion

    #region State
    protected EnemyMoveState moveState = EnemyMoveState.attaching;  // 敵の移動ステート
    protected bool isAttached = false;                              // 近づいているかどうかのフラグ 
    protected Vector3 avoidVelocity = new Vector3(0f, 0f, 0f);      // 避ける速度
    protected bool obstructedWall = false;                          // 壁に遮られているかどうか
    protected Transform currentTarget;                              // 現在のターゲット
    protected const string speedStatName = "SPEED";
    [SerializeField] protected Transform targetTower;               // ターゲットのタワー
    [SerializeField] protected EnemyProgressData enemyProgressData; // 敵のデータ
    #endregion

    /// @brief 移動を管理する関数 (子で上書き） 
    protected abstract void ManageMoving();

    /// @brief 変数を初期化する関数（子で上書き）
    protected abstract void InitValue();

    /// @brief ターゲットへの接近処理を行う関数
    protected void AttachTower()
    {
        Debug.Log(followSpeed);
        // 一定の速さで移動させる
        transform.position = Vector3.MoveTowards(
            transform.position,
            currentTarget.position,
            followSpeed * Time.deltaTime
        );
    }

    /// @brief 一定距離に近づいたか確認する関数
    protected void CheckAttaced()
    {
        if (Vector2.Distance(transform.position, currentTarget.position) < 1f)
        {
            isAttached = true;
            moveState = EnemyMoveState.idle;
        }
        else
        {
            isAttached = false;
            moveState = EnemyMoveState.attaching;
        }
    }

    /// @brief 壁があるか判定する関数
    protected void CheckIsWall()
    {
        //Wallのレイヤーを取得
        int wallLayerMask = LayerMask.GetMask("Wall");

        // 敵とタワーの直線上の間にWallのレイヤーオブジェクトがあるかをチェック
        RaycastHit2D hit = Physics2D.Linecast(transform.position, currentTarget.position, wallLayerMask);

        // もし壁に遮られていたら、横に移動
        if (hit.collider != null)
        {
            if (!obstructedWall)
            {
                DecideAvoidVelocity(hit.point);
            }
            transform.position = transform.position + (avoidVelocity * Time.deltaTime);
            followSpeed = 0f;
        }
        else
        {
            obstructedWall = false;
            followSpeed = settingSpeed;
        }
    }

    /// @brief 座標によってよける向きや速度を決める関数
    protected void DecideAvoidVelocity(Vector3 point)
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

        obstructedWall = true;
    }

    /// @brief ターゲットのタワーを設定する関数
    public void SetTargetTower(Transform tower)
    {
        targetTower = tower;
    }
}
