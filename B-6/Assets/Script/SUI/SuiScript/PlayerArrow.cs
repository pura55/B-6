using UnityEngine;

public class PlayerArrow : MonoBehaviour
{

    [SerializeField] private GameObject arrow;

    void Update()
    {

        GameObject[] rocks = GameObject.FindGameObjectsWithTag("Rock");

        //岩がない
        if (rocks.Length == 0)
        {
            arrow.SetActive(false);
            return;
        }

        //一番近い岩を探す
        GameObject nearstRock = null;
        float nearstDistance = Mathf.Infinity;

        foreach (GameObject rock in rocks)
        {
            float distance = Vector2.Distance
            (
                transform.position,
                rock.transform.position
            );

            if (distance < nearstDistance)
            {
                nearstDistance = distance;
                nearstRock = rock;
            }
        }

        //矢印を表示
        arrow.SetActive(true);

        //プレイヤー　→　岩の方向
        Vector2 direction = nearstRock.transform.position - transform.position;

        //矢印を回転
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        arrow.transform.rotation = Quaternion.Euler(0, 0, angle);

        //危険マークを表示

    }
}
