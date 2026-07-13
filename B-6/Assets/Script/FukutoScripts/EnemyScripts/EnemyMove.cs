using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    enum EnemyMoveState
    {
        idle,      // idle
        attaching, // 接近
    }

    #region Config
    private float followSpeed = 1f; // 追従速度
    #endregion

    #region State
    [SerializeField] private Transform targetTower;              // ターゲットのタワー
    private EnemyMoveState moveState = EnemyMoveState.attaching; // 敵の移動ステート
    private bool isAttached = false;                             // 近づいているかどうかのフラグ 
    private Vector3 avoidVelocity = new Vector3(0f, 0f, 0f);     // 避ける速度
    #endregion


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    　　// あらかじめ避ける向きや速度を決定する
        DecideAvoidVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        switch(moveState)
        {
            case EnemyMoveState.idle:
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
                  　　　targetTower.position, 
            　　　　　　followSpeed * Time.deltaTime
        );
    }

    // 一定距離に近づいたか確認する関数
    private void CheckAttaced()
    {
        if (Vector2.Distance(transform.position, targetTower.position) < 1f)
        {
            isAttached = true;
            moveState = EnemyMoveState.idle;
        }
    }

    // 壁があるか判定する関数
    private void CheckIsWall()
    {
        //Wallのレイヤーを取得
        int wallLayerMask = LayerMask.GetMask("Wall");

        // 敵とタワーの直線上の間にWallのレイヤーオブジェクトがあるかをチェック
        RaycastHit2D hit = Physics2D.Linecast(transform.position, targetTower.position, wallLayerMask);

        // もし壁に遮られていたら、横に移動
        if (hit.collider != null)
        {
            transform.position = transform.position + (avoidVelocity * Time.deltaTime);
            followSpeed = 0f;
        }
        else
        {
            followSpeed = 1f;
        }
    }

    // 座標によってよける向きや速度を決める関数
    private void DecideAvoidVelocity()
    {
        // XY軸の避ける向きと速度
        float avoidVelocityX = 3f;
        float avoidVelocityY = 0f;
        // 決めた値を代入
        avoidVelocity = new Vector3(avoidVelocityX, avoidVelocityY, 0f);

        //// x軸, y軸のランダムな符号の指数を決める
        //int randomSignIndexX = Random.Range(0, 2);
        //int randomSignIndexY = Random.Range(0, 2);

        ////　指数を基に速度を設定
        //if (randomSignIndexX == 0)
        //{
        //    avoidVelocityX = 2f;
        //}
        //else
        //{
        //    avoidVelocityX = -2f;
        //}

        ////　指数を基に速度を設定
        //if (randomSignIndexY == 0)
        //{
        //    avoidVelocityY = 2f;
        //}
        //else
        //{
        //    avoidVelocityY = -2f;
    }

    // ターゲットのタワーを設定する関数
    public void SetTargetTower(Transform tower)
    {
        targetTower = tower;
    }
}
