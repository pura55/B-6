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
            Debug.Log("W‰Ÿ‚µ‚Ä‚é");
        }
        if (Keyboard.current.sKey.isPressed)
        {
            move.y = -1;
            Debug.Log("S‰Ÿ‚µ‚Ä‚é");
        }
        if (Keyboard.current.aKey.isPressed)
        {
            move.x = -1;
            Debug.Log("A‰Ÿ‚µ‚Ä‚é");
        }
        if (Keyboard.current.dKey.isPressed)
        {
            move.x = 1;
            Debug.Log("D‰Ÿ‚µ‚Ä‚é");
        }

        Vector3 pos = transform.position;


        pos+= (Vector3)(move.normalized * speed * Time.deltaTime);


        pos.x = Mathf.Clamp(pos.x, -8.2f, 8.2f);
        pos.y = Mathf.Clamp(pos.y, -4.5f, 4.5f);

        transform.position = pos;

    }

}