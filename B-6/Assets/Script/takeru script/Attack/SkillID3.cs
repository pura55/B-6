using UnityEngine;
using UnityEngine.InputSystem;

public class SkillID3 : MonoBehaviour
{
    [Header("キャラ3 風スキル")]
    [SerializeField] private GameObject windPrefab;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifeTime = 3f;

    //スキルの弾数
    [SerializeField] private int bulletCount = 1;

    [Header("スキルストック")]
    [SerializeField] private SkillStock skillStock;

    //弾数を増やす
    public void AddBulletCount(int value)
    {
        bulletCount += value;

        Debug.Log(
            $"<color=cyan>キャラ3 スキル弾数アップ！</color> {bulletCount}発"
        );
    }

    public bool UseSkill()
    {
        if (skillStock == null)
        {
            Debug.LogWarning("SkillStockが設定されていません");
            return false;
        }

        if (windPrefab == null)
        {
            Debug.LogWarning("ID3のWind Prefabが設定されていません");
            return false;
        }

        if (Mouse.current == null || Camera.main == null)
            return false;

        if (!skillStock.UseStock())
            return false;

        Vector3 mousePos =
            Mouse.current.position.ReadValue();

        mousePos.z =
            -Camera.main.transform.position.z;

        Vector3 worldPos =
            Camera.main.ScreenToWorldPoint(mousePos);

        worldPos.z = 0f;

        Vector2 direction =
            (worldPos - transform.position).normalized;

        float angle =
            Mathf.Atan2(direction.y, direction.x)
            * Mathf.Rad2Deg;


        //弾数分だけ風を生成
        for (int i = 0; i < bulletCount; i++)
        {
            float offsetAngle = 0f;

            //複数発なら扇状にする
            if (bulletCount > 1)
            {
                float totalSpread = 30f;

                offsetAngle =
                    Mathf.Lerp(
                        -totalSpread / 2f,
                        totalSpread / 2f,
                        (float)i / (bulletCount - 1)
                    );
            }

            Vector2 windDirection =
                Quaternion.Euler(
                    0f,
                    0f,
                    offsetAngle
                ) * direction;


            GameObject wind = Instantiate(
                windPrefab,
                transform.position,
                Quaternion.Euler(
                    0f,
                    0f,
                    angle + offsetAngle - 90f
                )
            );

            WindWave windWave =
                wind.GetComponent<WindWave>();

            if (windWave != null)
            {
                windWave.Setup(
                    windDirection,
                    speed,
                    lifeTime
                );
            }
        }


        Debug.Log(
            "<color=green>【ID3 風スキル発動】</color>"
        );

        return true;
    }
}
