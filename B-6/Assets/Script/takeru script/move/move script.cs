using UnityEngine;
using UnityEngine.InputSystem;

public class MoveScript : MonoBehaviour
{
    [SerializeField] public float speed;

    private Vector2 move;

    private ID1Sprite playerAnimation;
    private SpriteRenderer spriteRenderer;
    private PlayerAttack playerAttack;

    public bool IsMoving()
    {
        return move != Vector2.zero;
    }

    void Start()
    {
        playerAnimation = GetComponent<ID1Sprite>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    void Update()
    {
        move = Vector2.zero;

        if (Keyboard.current == null)
            return;

        // 上
        if (Keyboard.current.wKey.isPressed)
        {
            move.y = 1;
        }

        // 下
        if (Keyboard.current.sKey.isPressed)
        {
            move.y = -1;
        }

        // 左
        if (Keyboard.current.aKey.isPressed)
        {
            move.x = -1;

            // 攻撃中じゃない時だけ左を向く
            if (!playerAttack.IsAttacking())
            {
                spriteRenderer.flipX = true;
            }
        }

        // 右
        if (Keyboard.current.dKey.isPressed)
        {
            move.x = 1;

            // 攻撃中じゃない時だけ右を向く
            if (!playerAttack.IsAttacking())
            {
                spriteRenderer.flipX = false;
            }
        }

        // =========================
        // 移動
        // =========================

        Vector3 pos = transform.position;

        pos +=
            (Vector3)(move.normalized * speed * Time.deltaTime);

        Vector3 camPos =
            Camera.main.transform.position;

        float halfHeight =
            Camera.main.orthographicSize;

        float halfWidth =
            halfHeight * Camera.main.aspect;

        float marginX = 0.5f;
        float marginY = 0.5f;

        pos.x = Mathf.Clamp(
            pos.x,
            camPos.x - halfWidth + marginX,
            camPos.x + halfWidth - marginX
        );

        pos.y = Mathf.Clamp(
            pos.y,
            camPos.y - halfHeight + marginY,
            camPos.y + halfHeight - marginY
        );

        transform.position = pos;

        // =========================
        // アニメーション
        // =========================

        if (playerAnimation.GetCurrentState() != ID1Sprite.PlayerAnimState.Attack &&
            playerAnimation.GetCurrentState() != ID1Sprite.PlayerAnimState.TakeHit &&
            playerAnimation.GetCurrentState() != ID1Sprite.PlayerAnimState.Skill &&
            playerAnimation.GetCurrentState() != ID1Sprite.PlayerAnimState.Death)
        {
            if (move != Vector2.zero)
            {
                playerAnimation.ChangeState(
                    ID1Sprite.PlayerAnimState.Move
                );
            }
            else
            {
                playerAnimation.ChangeState(
                    ID1Sprite.PlayerAnimState.Idle
                );
            }
        }
    }
}