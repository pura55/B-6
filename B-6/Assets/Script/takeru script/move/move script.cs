using UnityEngine;
using UnityEngine.InputSystem;

public class MoveScript : MonoBehaviour
{
    [SerializeField] float speed;

    private Vector2 move;

    void Start()
    {
    }

    void Update()
    {
        move = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
        {
            move.y = 1;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            move.y = -1;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            move.x = -1;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            move.x = 1;
        }

        Vector3 pos = transform.position;


        pos+= (Vector3)(move.normalized * speed * Time.deltaTime);

        //カメラ端を取得
        Vector3 camPos = Camera.main.transform.position;

        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = halfHeight * Camera.main.aspect;

        float marginX = 0.5f;
        float marginY = 0.5f;

        //プレイヤーを画面内に制限
        pos.x = Mathf.Clamp(pos.x, camPos.x - halfWidth + marginX, camPos.x + halfWidth - marginX);
        pos.y = Mathf.Clamp(pos.y, camPos.y - halfHeight+ marginY, camPos.y + halfHeight - marginY);

        transform.position = pos;

    }

}