using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤーヘルス
/// 
/// プレイヤーの体力クラス
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    #region Config
    private int myHp = 5; // HP
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
    public void ReceiveDamage(int dmg)
    {
        myHp -= dmg;

        // HPが0以下なら死亡
        if (myHp <= 0)
        {
            playerAnimation.ChangeState(ID1Sprite.PlayerAnimState.Death);
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

    public void Respawn()
    {
        // 初期位置に戻す
        transform.position = startPosition;

        // HPを初期値に戻す
        myHp = playerProgressData.hp;
    }
}

