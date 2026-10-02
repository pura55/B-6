using UnityEngine;
using UnityEngine.UI;

public class PlayerArrow : MonoBehaviour
{
    [Header("矢印")]
    [SerializeField] private Image arrowImage;

    [Header("危険マーク")]
    [SerializeField] private GameObject dangerMark;

    [Header("落石警告ライン")]
    [SerializeField] private GameObject warningLine;

    [Header("タワー")]
    [SerializeField] private Transform tower;

    [Header("警告ライン点滅")]
    [SerializeField] private float warningBlinkSpeed = 0.5f;

    private float warningBlinkTimer = 0f;

    void Start()
    {
        // ゲーム開始時は非表示
        arrowImage.gameObject.SetActive(false);
        dangerMark.SetActive(false);
        warningLine.SetActive(false);
    }

    void Update()
    {
        GameObject[] rocks = GameObject.FindGameObjectsWithTag("Rock");

        // 岩がない
        if (rocks.Length == 0)
        {
            arrowImage.gameObject.SetActive(false);
            dangerMark.SetActive(false);
            warningLine.SetActive(false);

            warningBlinkTimer = 0f;

            return;
        }

        // 岩があるので表示
        arrowImage.gameObject.SetActive(true);
        dangerMark.SetActive(true);

        // 警告ライン点滅
        warningBlinkTimer += Time.deltaTime;

        if (warningBlinkTimer >= warningBlinkSpeed)
        {
            warningBlinkTimer = 0f;

            warningLine.SetActive(!warningLine.activeSelf);
        }

        // タワーに一番近い岩を探す
        GameObject nearestRock = null;
        float nearestDistance = Mathf.Infinity;

        foreach (GameObject rock in rocks)
        {
            float distance = Vector2.Distance(
                tower.position,
                rock.transform.position
            );

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestRock = rock;
            }
        }

        // プレイヤー → 岩の方向
        Vector2 direction =
            nearestRock.transform.position - transform.position;

        // 方向を正規化
        direction.Normalize();

        // プレイヤーの位置を画面座標に変換
        Vector3 screenPos =
            Camera.main.WorldToScreenPoint(transform.position);

        // プレイヤーから200ピクセル離した位置に矢印を表示
        screenPos.x += direction.x * 200f;
        screenPos.y += direction.y * 200f;

        // 矢印の位置を設定
        arrowImage.rectTransform.position = screenPos;

        // 角度を計算
        float angle =
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 矢印を岩の方向に回転
        arrowImage.rectTransform.rotation =
            Quaternion.Euler(0, 0, angle);

        // 警告ラインの位置
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // 横方向
            if (direction.x > 0)
            {
                // 右
                warningLine.transform.position =
                    new Vector3(
                        Screen.width - 20f,
                        Screen.height / 2f,
                        0
                    );

                warningLine.transform.rotation =
                    Quaternion.Euler(0, 0, 0);
            }
            else
            {
                // 左
                warningLine.transform.position =
                    new Vector3(
                        20f,
                        Screen.height / 2f,
                        0
                    );

                warningLine.transform.rotation =
                    Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            // 縦方向
            if (direction.y > 0)
            {
                // 上
                warningLine.transform.position =
                    new Vector3(
                        Screen.width / 2f,
                        Screen.height - 20f,
                        0
                    );

                warningLine.transform.rotation =
                    Quaternion.Euler(0, 0, 90f);
            }
            else
            {
                // 下
                warningLine.transform.position =
                    new Vector3(
                        Screen.width / 2f,
                        20f,
                        0
                    );

                warningLine.transform.rotation =
                    Quaternion.Euler(0, 0, 90f);
            }
        }
    }
}