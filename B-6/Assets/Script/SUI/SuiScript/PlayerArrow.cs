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
        // =========================
        // 非表示のオブジェクトも含めて探す
        // =========================
        GameObject[] allObjects =
            Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // Scene上のオブジェクトだけ
            if (!obj.scene.IsValid())
                continue;

            // 矢印
            if (obj.name == "ArrowImage")
            {
                arrowImage =
                    obj.GetComponent<Image>();
            }

            // 危険マーク
            if (obj.name == "DANGERMARK")
            {
                dangerMark = obj;
            }

            // 警告ライン
            if (obj.name == "RockWarningLine")
            {
                warningLine = obj;
            }

            // タワー
            if (obj.name == "TOWER")
            {
                tower = obj.transform;
            }
        }


        // =========================
        // 最初は非表示
        // =========================
        if (arrowImage != null)
            arrowImage.gameObject.SetActive(false);

        if (dangerMark != null)
            dangerMark.SetActive(false);

        if (warningLine != null)
            warningLine.SetActive(false);


        // =========================
        // 確認
        // =========================
        if (arrowImage == null)
            Debug.LogWarning("ArrowImageが見つかりません");

        if (dangerMark == null)
            Debug.LogWarning("DangerMarkが見つかりません");

        if (warningLine == null)
            Debug.LogWarning("RockWarningLineが見つかりません");

        if (tower == null)
            Debug.LogWarning("TOWERが見つかりません");
    }


    void Update()
    {
        // =========================
        // 必要なものがない場合
        // =========================
        if (arrowImage == null ||
            dangerMark == null ||
            warningLine == null ||
            tower == null)
        {
            return;
        }


        // =========================
        // 岩を取得
        // =========================
        GameObject[] rocks =
            GameObject.FindGameObjectsWithTag("Rock");


        // =========================
        // 岩がない
        // =========================
        if (rocks.Length == 0)
        {
            arrowImage.gameObject.SetActive(false);

            dangerMark.SetActive(false);

            warningLine.SetActive(false);

            warningBlinkTimer = 0f;

            return;
        }


        // =========================
        // 岩がある
        // =========================
        arrowImage.gameObject.SetActive(true);

        dangerMark.SetActive(true);


        // =========================
        // 警告ライン点滅
        // =========================
        warningBlinkTimer +=
            Time.deltaTime;

        if (warningBlinkTimer >=
            warningBlinkSpeed)
        {
            warningBlinkTimer = 0f;

            warningLine.SetActive(
                !warningLine.activeSelf
            );
        }


        // =========================
        // タワーに一番近い岩を探す
        // =========================
        GameObject nearestRock = null;

        float nearestDistance =
            Mathf.Infinity;


        foreach (GameObject rock in rocks)
        {
            float distance =
                Vector2.Distance(
                    tower.position,
                    rock.transform.position
                );

            if (distance < nearestDistance)
            {
                nearestDistance =
                    distance;

                nearestRock =
                    rock;
            }
        }


        // 念のため
        if (nearestRock == null)
        {
            return;
        }


        // =========================
        // プレイヤー → 岩の方向
        // =========================
        Vector2 direction =
            nearestRock.transform.position
            - transform.position;

        direction.Normalize();


        // =========================
        // プレイヤー位置を画面座標へ
        // =========================
        if (Camera.main == null)
        {
            return;
        }

        Vector3 screenPos =
            Camera.main.WorldToScreenPoint(
                transform.position
            );


        // =========================
        // プレイヤーから200px離す
        // =========================
        screenPos.x +=
            direction.x * 200f;

        screenPos.y +=
            direction.y * 200f;


        // =========================
        // 矢印の位置
        // =========================
        arrowImage.rectTransform.position =
            screenPos;


        // =========================
        // 矢印の角度
        // =========================
        float angle =
            Mathf.Atan2(
                direction.y,
                direction.x
            )
            * Mathf.Rad2Deg;


        arrowImage.rectTransform.rotation =
            Quaternion.Euler(
                0f,
                0f,
                angle
            );


        // =========================
        // 警告ラインの位置
        // =========================

        // 横方向の方が強い
        if (Mathf.Abs(direction.x) >
            Mathf.Abs(direction.y))
        {
            // 右
            if (direction.x > 0)
            {
                warningLine.transform.position =
                    new Vector3(
                        Screen.width - 20f,
                        Screen.height / 2f,
                        0f
                    );
            }

            // 左
            else
            {
                warningLine.transform.position =
                    new Vector3(
                        20f,
                        Screen.height / 2f,
                        0f
                    );
            }


            warningLine.transform.rotation =
                Quaternion.Euler(
                    0f,
                    0f,
                    0f
                );
        }

        // 縦方向の方が強い
        else
        {
            // 上
            if (direction.y > 0)
            {
                warningLine.transform.position =
                    new Vector3(
                        Screen.width / 2f,
                        Screen.height - 20f,
                        0f
                    );
            }

            // 下
            else
            {
                warningLine.transform.position =
                    new Vector3(
                        Screen.width / 2f,
                        20f,
                        0f
                    );
            }


            warningLine.transform.rotation =
                Quaternion.Euler(
                    0f,
                    0f,
                    90f
                );
        }
    }
}