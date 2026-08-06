using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRespawn : MonoBehaviour
{
    [Header("HP")]
    public int hp = 10;

    void Update()
    {
        // デバッグ用：PキーでHPを0にする
        if (Keyboard.current != null &&
            Keyboard.current.pKey.wasPressedThisFrame)
        {
            hp = 0;

            Debug.Log("デバッグ：HPを0にしました");

            if (hp <= 0)
            {
                Respawn();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        Debug.Log("HP：" + hp);

        if (hp <= 0)
        {
            Respawn();
        }
    }


    void Respawn()
    {
        hp = 10;

        // 座標0,0へ戻す
        transform.position = Vector3.zero;

        Debug.Log("0,0にリスポーン！");
    }
}