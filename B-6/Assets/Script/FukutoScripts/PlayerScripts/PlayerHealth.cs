using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// プレイヤーヘルス
/// 
/// プレイヤーの体力クラス
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    #region Config
    public float maxHP = 5; //最大HP
    private float myHp; // HP
    public float respawnTime = 5.0f;//リスポーン時間
    public int killHeal = 0;// キルヒール
    #endregion

    #region State
    [SerializeField] private PlayerProgressData playerProgressData; // プレイヤーのデータ
    [SerializeField] private Slider hpSlider;
    #endregion

    private ID1Sprite playerAnimation;
    private Vector3 startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // データからHPを取得
        myHp = playerProgressData.hp;

        // HPバーの初期設定
        hpSlider.maxValue = maxHP;
        hpSlider.value = myHp;

        playerAnimation = GetComponent<ID1Sprite>();

        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current != null &&
       Keyboard.current.lKey.wasPressedThisFrame)
        {
            Debug.Log("Lキー押した");
            ReceiveDamage(1);
        }

        // デバッグ用：Pキーで即死
        if (Keyboard.current != null &&
            Keyboard.current.pKey.wasPressedThisFrame)
        {
            ReceiveDamage(myHp);
        }
    }

    /// @brief 被ダメージ処理を行う関数
    public void ReceiveDamage(float dmg)
    {
        myHp -= dmg;

        // HPバー更新
        hpSlider.value = myHp;

        if (myHp <= 0)
        {
            playerAnimation.ChangeState(ID1Sprite.PlayerAnimState.Death);
            StartCoroutine(RespawnCoroutine());
        }
        else
        {
            playerAnimation.ChangeState(ID1Sprite.PlayerAnimState.TakeHit);
        }
    }

    /// @brief 生死を判定するフラグ
    public bool IsAlive()
    {
        // hpが0だったらfalse
        if (myHp <= 0)
            return false;
        else
            return true;
    }

    // リスポーン処理
    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(respawnTime);

        Respawn();
    }

    //プレイヤーをリスポーンさせる
    public void Respawn()
    {
        // 初期位置に戻す
        transform.position = startPosition;

        // 現在の最大HPまで回復
        myHp = maxHP;

        hpSlider.value = myHp;

        // HPを初期値に戻す
        //myHp = playerProgressData.hp;
        //↑これがあると、初期値に戻したときに最大HPが反映されなくなる
    }

    // 敵を倒したときの回復
    public void KillHeal()
    {
        if (killHeal <= 0)
            return;

        myHp += killHeal;

        // 最大HPを超えないようにする
        if (myHp > maxHP)
        {
            myHp = maxHP;
        }

        hpSlider.value = myHp;

        Debug.Log("キルヒール！ HP +" + killHeal);
    }

}

