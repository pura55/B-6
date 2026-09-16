using UnityEngine;

public class PlayerArrow : MonoBehaviour
{
    [Header("矢印")]
    [SerializeField] private GameObject arrow;

    [Header("危険マーク")]
    [SerializeField] private GameObject dangerMark;

    [Header("タワー")]
    [SerializeField] private Transform tower;

    void Update()
    {
        GameObject[] rocks = GameObject.FindGameObjectsWithTag("Rock");
        Debug.Log("見つかった岩の数：" + rocks.Length);

        // 岩がない
        if (rocks.Length == 0)
        {
            arrow.SetActive(false);
            dangerMark.SetActive(false);
            return;
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

        // 岩があるので表示
        arrow.SetActive(true);
        dangerMark.SetActive(true);

        // プレイヤー → 一番近い岩の方向
        Vector2 direction =
            nearestRock.transform.position - transform.position;

        // 矢印を回転
        float angle =
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        arrow.transform.rotation =
            Quaternion.Euler(0, 0, angle);
    }
}