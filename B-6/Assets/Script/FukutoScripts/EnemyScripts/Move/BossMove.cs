using UnityEngine;

public class BossMove : AggressiveMove
{
    #region Config
    private bool isHit = false;
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitValue();
    }

    // Update is called once per frame
    void Update()
    {
        ManageMoving();
    }

    protected override void ManageMoving()
    {
        if (isHit)
        {
            // ターゲットの切り替え
            ConvertTarget();
        }

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

    protected override void InitValue()
    {
        // 設定速度にデータから取得したスピードを格納
        settingSpeed = enemyProgressData.GetFloatStat(enemyID, speedStatName);
        followSpeed = settingSpeed;
        currentTarget = targetTower;
    }

    // 親のクラスから継承して親の処理の間にこの関数を加える
    protected override void ResetHitFlag()
    {
        isHit = false;
    }

    public void SetIsHit(bool hit)
    {
        isHit = hit;
    }
}
