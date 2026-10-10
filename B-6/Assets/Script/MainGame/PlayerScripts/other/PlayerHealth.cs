using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// プレイヤーヘルス
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    #region Config

    public float maxHP = 5;
    private float myHp = 0;

    // 基本リスポーン時間
    public float respawnTime = 5.0f;

    // 死亡するたびに増える時間
    public float respawnTimeIncrease = 2.0f;

    public int killHeal = 0;

    #endregion


    #region State

    [SerializeField] private PlayerProgressData playerProgressData;
    [SerializeField] private Slider hpSlider;

    private DamageTextManager damageTextManager; // ダメージ表示

    #endregion


    private ID1Sprite playerAnimation;
    private Vector3 startPosition;

    // 入力禁止を管理
    private PlayerInputLock inputLock;

    // 死亡中かどうか
    public bool IsDead { get; private set; } = false;

    // リスポーン終了時刻
    private float respawnEndTime = 0f;

    // 現在のリスポーン時間
    private float currentRespawnTime;

    // 死亡回数
    private int deathCount = 0;


    void Start()
    {
        // PlayerInputLockを取得
        inputLock = GetComponent<PlayerInputLock>();


        // データからHP取得
        maxHP = playerProgressData.hp;
        myHp = maxHP;


        // リスポーン時間を初期値にする
        currentRespawnTime = respawnTime;


        // Scene上のPlayerHPを取得
        if (hpSlider == null)
        {
            GameObject hpObject =
                GameObject.Find("PlayerHP");

            if (hpObject != null)
            {
                hpSlider =
                    hpObject.GetComponent<Slider>();
            }
        }


        // HPバー初期設定
        UpdateHPSlider();


        playerAnimation =
            GetComponent<ID1Sprite>();


        startPosition =
            transform.position;
    }


    void Update()
    {
        // 死亡中はデバッグ入力も受け付けない
        if (IsDead)
            return;
    }


    /// <summary>
    /// 最大HPアップ
    /// </summary>
    public void IncreaseMaxHP(float value)
    {
        maxHP += value;

        // HPゲージを更新
        UpdateHPSlider();

        Debug.Log("最大HP +" + value);
    }


    /// <summary>
    /// HPゲージ更新
    /// </summary>
    private void UpdateHPSlider()
    {
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHP;
            hpSlider.value = myHp;
        }
        else
        {
            Debug.LogWarning(
                "PlayerHPのSliderが見つかりません"
            );
        }
    }


    /// <summary>
    /// ダメージ処理
    /// </summary>
    public void ReceiveDamage(float dmg)
    {
        // 死亡中はダメージを受けない
        if (IsDead)
            return;


        Debug.Log(
            "被ダメ前プレイヤーHP: " +
            myHp
        );


        myHp -= dmg;


        Debug.Log(
            "受けるダメージ: -" +
            dmg
        );

        // ダメージ表示
        damageTextManager.ShowDamageText(transform.position, (int)dmg, DamageTextManager.Target.PLAYER);

        // HPがマイナスにならないようにする
        if (myHp < 0)
        {
            myHp = 0;
        }


        // HPバー更新
        UpdateHPSlider();


        // 死亡
        if (myHp <= 0)
        {
            Debug.Log("プレイヤー死亡！");
            Die();
        }
        else
        {
            // 被ダメージアニメーション
            playerAnimation.ChangeState(
                ID1Sprite.PlayerAnimState.TakeHit
            );
        }
    }


    /// <summary>
    /// 死亡処理
    /// </summary>
    private void Die()
    {
        // すでに死亡していたら何もしない
        if (IsDead)
            return;


        // 死亡回数を増やす
        deathCount++;


        // 1回目は基本時間のまま
        // 2回目以降は死亡するたびに＋2秒
        currentRespawnTime =
            respawnTime +
            (deathCount - 1) * respawnTimeIncrease;


        // 死亡状態
        IsDead = true;


        // 現在のリスポーン時間から終了時刻を計算
        respawnEndTime =
            Time.time + currentRespawnTime;


        // =========================
        // 入力を禁止
        // =========================

        if (inputLock != null)
        {
            inputLock.LockInput();
        }


        // =========================
        // 死亡アニメーション
        // =========================

        playerAnimation.ChangeState(
            ID1Sprite.PlayerAnimState.Death
        );


        Debug.Log(
            "<color=red>プレイヤー死亡</color> " +
            "死亡回数: " +
            deathCount +
            " / リスポーンまで: " +
            currentRespawnTime +
            "秒"
        );


        // =========================
        // リスポーン開始
        // =========================

        StartCoroutine(
            RespawnCoroutine()
        );
    }


    /// <summary>
    /// 生きているか
    /// </summary>
    public bool IsAlive()
    {
        return !IsDead;
    }


    /// <summary>
    /// リスポーン待機時間の割合
    /// </summary>
    public float GetRespawnTimeRate()
    {
        if (!IsDead)
            return 0f;


        if (currentRespawnTime <= 0f)
            return 0f;


        float remain =
            respawnEndTime - Time.time;


        return 1f - Mathf.Clamp01(
            remain / currentRespawnTime
        );
    }


    /// <summary>
    /// リスポーン待機
    /// </summary>
    private IEnumerator RespawnCoroutine()
    {
        Debug.Log(
            "リスポーンまで " +
            currentRespawnTime +
            " 秒"
        );


        yield return new WaitForSeconds(
            currentRespawnTime
        );


        Respawn();
    }


    /// <summary>
    /// リスポーン
    /// </summary>
    public void Respawn()
    {
        // 初期位置へ戻す
        transform.position = startPosition;


        // HP全回復
        myHp = maxHP;


        // HPバー更新
        UpdateHPSlider();


        // 生存状態に戻す
        IsDead = false;


        // 入力禁止解除
        if (inputLock != null)
        {
            inputLock.UnlockInput();
        }


        // リスポーンしたのでIdleへ
        playerAnimation.ChangeState(
            ID1Sprite.PlayerAnimState.Idle
        );


        Debug.Log(
            "<color=green>プレイヤーリスポーン</color>"
        );
    }


    /// <summary>
    /// 敵を倒したときの回復
    /// </summary>
    public void KillHeal()
    {
        // 死亡中は回復しない
        if (IsDead)
            return;


        if (killHeal <= 0)
            return;


        myHp +=
            killHeal;


        // 最大HPを超えない
        if (myHp > maxHP)
        {
            myHp = maxHP;
        }


        // HPバー更新
        UpdateHPSlider();
    }

    /// @brief ダメージテキストマネージャーを設定する関数
    public void SetDamageText(DamageTextManager damageText)
    {
        damageTextManager = damageText;
    }
}