using UnityEngine;

public class SuiPlayerMOve : MonoBehaviour
{
    // 移動速度
    [SerializeField] private float moveSpeed = 5f;

    void Update()
    {
        // WASD入力を取得
        float x = Input.GetAxisRaw("Horizontal"); // A,D
        float y = Input.GetAxisRaw("Vertical");   // W,S

        // 移動方向を作成
        Vector3 moveDirection = new Vector3(x, y, 0f);

        // プレイヤーを移動
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}