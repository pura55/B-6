using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


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
    #endregion

    #region State
    [SerializeField] private PlayerProgressData playerProgressData; // プレイヤーのデータ
    #endregion

    private ID1Sprite playerAnimation;
    private Vector3 startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // データからHPを取得
        myHp = playerProgressData.hp;

        playerAnimation = GetComponent<ID1Sprite>();

        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // デバッグ用：OキーでTakeHitアニメだけ確認
        if (Keyboard.current != null &&
            Keyboard.current.oKey.wasPressedThisFrame)
        {
            playerAnimation.ChangeState(ID1Sprite.PlayerAnimState.TakeHit);
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

        // HPが0以下なら死亡
        if (myHp <= 0)
        {
            playerAnimation.ChangeState(ID1Sprite.PlayerAnimState.Death);

            // リスポーン開始
            StartCoroutine(RespawnCoroutine());
        }
        // まだ生きているならダメージ
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

        // HPを初期値に戻す
        //myHp = playerProgressData.hp;
        //↑これがあると、初期値に戻したときに最大HPが反映されなくなる
    }
}

