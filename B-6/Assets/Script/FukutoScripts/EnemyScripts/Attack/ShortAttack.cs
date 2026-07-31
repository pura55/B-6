using UnityEngine;

/// <summary>
/// ショートディスタンスアタック
/// 
/// 敵の近接攻撃処理を行うクラス
/// </summary>
public class ShortAttack : BaseEnemyAttack
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitValue();
    }

    // Update is called once per frame
    void Update()
    {
        ManageAttacking();
    }

    protected override void ManageAttacking()
    {
        switch (attackState)
        {
            case EnemyAttackState.idle:
                break;
            case EnemyAttackState.attacking:
                
                break;
            case EnemyAttackState.recast:
                if (isAttacked)
                    CompleteInterval();
                break;
        }
    }

    protected override void InitValue()
    {
        statAtk = enemyProgressData.GetIntStat(enemyID, attackStatName);
        recastInterval = enemyProgressData.GetFloatStat(enemyID, intervalStatName);
        attackState = EnemyAttackState.idle;
    }

}
