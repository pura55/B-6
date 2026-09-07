using UnityEngine;

/// <summary>
/// ウェポンスキル
/// 
/// 武器スキルのクラス
/// </summary>
public class WeaponSkill : BaserEnemySkill
{
    #region Config
    protected float activationInterval = 0.2f; // 武器を発動する時間間隔
    #endregion

    #region State
    protected GameObject[] weapons; // 武器のオブジェクト
    protected float currentCountTime = 0f; //現在の間隔時間
    protected int activeWeaponCount = 0; // 有効にした武器のカウント
    #endregion

    private void Start()
    {
        InitValue();
    }

    private void Update()
    {
        ManageAttacking();
    }

    protected override void ManageAttacking()
    {
        switch (skillState)
        {
            case EnemySkillState.idle:
                break;
            case EnemySkillState.skill:
                UsingSkill();
                break;
            case EnemySkillState.recast:
                Recast();
                break;
        }
    }

    protected override void UsingSkill()
    {
        // スプライトの指数が攻撃ヒット時の番号と一致している場合
        if (midBossAnimation.GetSpriteIndex() == hitAnimationNumber) isAttacked = true;

        if (isAttacked)
        {
            // 武器を有効
            ActiveWeapon();
        }
    }

    protected override void InitValue()
    {
        statSkill = enemyProgressData.GetIntStat(enemyID, skillStatName);
        recastInterval = enemyProgressData.GetFloatStat(enemyID, intervalStatName);
        midBossAnimation = gameObject.GetComponent<MidBossAnimation>();
        skillState = EnemySkillState.idle;
        SetWeapons();
    }

    protected override void Recast()
    {
        if (isAttacked)
            CompleteInterval();
        else
        {
            skillState = EnemySkillState.idle;
            activeWeaponCount = 0;
            return;
        }
    }

    /// @brief 武器を有効にする関数
    protected void ActiveWeapon()
    {
        // 有効にした数が総数未満だったら
        if (activeWeaponCount < weapons.Length)
        {
            // 一定間隔ごとに武器を順番に有効にしていく
            if(activationInterval < currentCountTime)
            {
                // 武器のアニメーションに対して許可を出す
                weapons[activeWeaponCount].GetComponent<WeaponAnimation>().SetPermissionAct(true);

                Debug.Log(activeWeaponCount);

                // アクティブにした武器数をカウント
                activeWeaponCount++;
                currentCountTime = 0;

                return;
            }

            currentCountTime += Time.deltaTime;
        }
        else
        {
            skillState = EnemySkillState.recast;
        }
    }

    /// @brief スキルに使用する武器をセットする関数
    protected void SetWeapons()
    {
        // このオブジェクトを数える変数
        int objectCount = 0;

        // 子のオブジェクトを順番に確認
        for (int i = 0; i< transform.childCount; i++)
        {
            if (!transform.GetChild(i).gameObject.CompareTag("Nail")) continue;
            objectCount++;
        }

        // 確認した子の大きさ分配列をリサイズ
        weapons = new GameObject[objectCount];

        // 一度リセット
        objectCount = 0;

        // このオブジェクトを配列に格納
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).gameObject.CompareTag("Nail")) continue;

            weapons[objectCount] = transform.GetChild(i).gameObject;
            // 原因を特定するためのデバッグコードの例
            if (weapons[objectCount] == null)
            {
                Debug.LogWarning("【警告】確認したい変数がnullです！");
            }

            objectCount++;
        }
    }
}
