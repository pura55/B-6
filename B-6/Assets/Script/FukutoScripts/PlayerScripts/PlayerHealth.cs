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

    public float respawnTime = 5.0f;
    public int killHeal = 0;

    #endregion


    #region State

    [SerializeField] private PlayerProgressData playerProgressData;
    [SerializeField] private Slider hpSlider;

    #endregion


    private ID1Sprite playerAnimation;
    private Vector3 startPosition;

    // 入力禁止を管理
    private PlayerInputLock inputLock;

    // 死亡中かどうか
    public bool IsDead { get; private set; } = false;


    void Start()
    {
        // PlayerInputLockを取得
        inputLock = GetComponent<PlayerInputLock>();


        // データからHP取得
        maxHP = playerProgressData.hp;
        myHp = maxHP;


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


        // デバッグ：Lキーで1ダメージ
        if (Keyboard.current != null &&
            Keyboard.current.lKey.wasPressedThisFrame)
        {
            Debug.Log("Lキー押した");

            ReceiveDamage(1);
        }


        // デバッグ：Pキーで即死
        if (Keyboard.current != null &&
            Keyboard.current.pKey.wasPressedThisFrame)
        {
            ReceiveDamage(myHp);
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


        // HPがマイナスにならないようにする
        if (myHp < 0)
        {
            myHp = 0;
        }


        // HPバー更新
        if (hpSlider != null)
        {
            hpSlider.value = myHp;
        }


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


        // 死亡状態
        IsDead = true;


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
            "<color=red>プレイヤー死亡</color>"
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
    /// リスポーン待機
    /// </summary>
    private IEnumerator RespawnCoroutine()
    {
        Debug.Log(
            "リスポーンまで " +
            respawnTime +
            " 秒"
        );


        yield return new WaitForSeconds(
            respawnTime
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
        if (hpSlider != null)
        {
            hpSlider.value = myHp;
        }

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


        if (hpSlider != null)
        {
            hpSlider.value =
                myHp;
        }


        Debug.Log(
            "キルヒール！ HP +" +
            killHeal
        );
    }
}
