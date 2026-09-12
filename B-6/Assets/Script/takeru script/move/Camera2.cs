using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2 : MonoBehaviour
{
    [SerializeField] Transform playerTr; // プレイヤーのTransform
    [SerializeField] Vector2 cameraMaxPos = new Vector2(5f, 5f); // カメラの右上限界点
    [SerializeField] Vector2 cameraMinPos = new Vector2(-5f, -5f); // カメラの左下限界点

    private void Update()
    {
        float x = Mathf.Clamp(
            playerTr.position.x,
            cameraMinPos.x,
            cameraMaxPos.x
        );

        float y = Mathf.Clamp(
            playerTr.position.y,
            cameraMinPos.y,
            cameraMaxPos.y
        );

        transform.position = new Vector3(
            x,
            y,
            -10f
        );
    }

}
