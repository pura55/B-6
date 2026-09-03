using UnityEngine;

/// <summary>
/// ベースヒットボックスローテーション
/// 
/// 攻撃判定の移動回転処理の基底クラス
/// </summary>
public class BaseHitBoxRotation : MonoBehaviour
{
    #region Config
    protected static float distanceRadius = 0.3f; // 距離半径
    [SerializeField] protected bool onNomalMove = false; // 基本移動が付いているかどうかのフラグ
    [SerializeField] protected bool onAggressiveMove = false; // プレイヤー追跡の移動がついているかどうかのフラグ
    [SerializeField] protected bool onBossMove = false; // ボスの移動フラグ
    #endregion

    #region State
    protected Quaternion targetRotation = Quaternion.Euler(0, 0f, 90f); // 目標回転量
    protected Vector3 targetPosition = Vector3.zero;                    // 目標座標
    [SerializeField]protected Vector3 parentHeaded = Vector3.zero; // 親が向かっている方角（符号を取得する為の変数）
    protected Vector3 distanceParent = Vector3.zero; // 親からの距離
    protected Transform parentTransform; // 親のトランスフォーム
    protected NomalMove nomalMove;       // 基本的な移動を行うスクリプト
    protected AggressiveMove aggressiveMove; // プレイヤーの追跡を行う移動スクリプト
    protected BossMove bossMove; // ボスの移動
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parentTransform = transform.parent;
        // 親スクリプトの参照を取得
        SetMoveScript();
    }

    // Update is called once per frame
    void Update()
    {
        DecideRotationalMovement();
        MoveHitBox();
        RotateHitBox();
    }

    ///@brief hitBoxの回転・移動を決定する関数
    protected void DecideRotationalMovement()
    {
        // 親が向かっている方角を取得
        GetParentHeaded();

        float numx = parentHeaded.x; // 親進行方向のx座標
        float numy = parentHeaded.y; // 親進行方向のy座標

        // アークタンジェント2を使ってラジアンを求める
        /// Atan2：Atanは比率から角度を求めるがtanの性質上象限を限定できないため
        ///        Atan2を使用し「0～90」「90～180」「-180～-90」「-90～0」
        ///        の範囲で4象限を特定できる
        float radian = Mathf.Atan2(numy, numx); 

        Vector3 target = Vector3.zero; // 移動の目標
        target.y = distanceRadius * Mathf.Sin(radian); // y = r * sin(θ)
        target.x = distanceRadius * Mathf.Cos(radian); // x = r * cos(θ)

        // 目標移動座標決定
        targetPosition = target;

        // 角度に変換
        float degree = radian * Mathf.Rad2Deg;

        // 目標回転量決定
        targetRotation = Quaternion.Euler(0, 0, degree);
    }

    ///@brief hitBoxの移動を行う関数
    protected void MoveHitBox()
    {
        transform.localPosition = targetPosition;
    }

    ///@brief hitBoxの回転を行う関数
    protected void RotateHitBox()
    {
        transform.rotation = targetRotation;
    }

    ///@brief 移動用スクリプトの参照を取得する関数
    protected void SetMoveScript()
    {
        if(onNomalMove)
        {
            nomalMove = parentTransform.GetComponent<NomalMove>();
        }
        else if(onAggressiveMove)
        {
            aggressiveMove = parentTransform.GetComponent<AggressiveMove>();
        }
        else if(onBossMove)
        {
            bossMove = parentTransform.GetComponent<BossMove>();
        }
    }

    /// @brief 親が向かっている方角を取得する関数
    protected void GetParentHeaded()
    {
        if (onNomalMove)
        {
            parentHeaded = nomalMove.GetMovementPerFrame();
        }
        else if(onAggressiveMove)
        {
            parentHeaded = aggressiveMove.GetMovementPerFrame();
        }
        else if (onBossMove)
        {
            parentHeaded = bossMove.GetMovementPerFrame();
        }
    }

}
