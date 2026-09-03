using UnityEngine;

/// <summary>
/// ロックロール
/// 
/// 落石が転がる処理を行う関数
/// </summary>
public class RockRoll : MonoBehaviour
{
    #region Config
    private float rollSpeed = 2f; // 追従速度
    #endregion

    #region State
    [SerializeField] private Transform targetTower; // ターゲットのタワー
    //private bool hitWall = false;
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        AttachTower();
    }

    /// @biref タワーへの接近処理を行う関数
    private void AttachTower()
    {
        // 一定の速さで移動させる
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetTower.position,
            rollSpeed * Time.deltaTime
        );
    }

    /// @brief ターゲットのタワーを設定する関数
    public void SetTargetTower(Transform tower)
    {
        targetTower = tower;
    }
}
