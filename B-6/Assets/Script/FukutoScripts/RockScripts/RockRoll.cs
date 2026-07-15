using UnityEngine;

public class RockRoll : MonoBehaviour
{
    #region Config
    private float rollSpeed = 1f; // 追従速度
    #endregion

    #region State
    [SerializeField] private Transform targetTower;              // ターゲットのタワー
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

    private void AttachTower()
    {
        // 一定の速さで移動させる
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetTower.position,
            rollSpeed * Time.deltaTime
        );
    }

    public void SetTargetTower(Transform tower)
    {
        targetTower = tower;
    }
}
