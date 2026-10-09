using UnityEngine;
using UnityEngine.InputSystem;

public class SkillID2 : MonoBehaviour
{
    [Header("キャラ2 スキル")]
    [SerializeField] private GameObject wavePrefab;

    [SerializeField] private float speed = 3f;
    [SerializeField] private float lifeTime = 10f;
    [SerializeField] private float waterRange = 8f;

    //スキルの弾数
    [SerializeField] private int bulletCount = 1;

    [Header("スキルストック")]
    [SerializeField] private SkillStock skillStock;

    [Header("SOUND")]
    [SerializeField] private SoundPlayer soundPlayer;
    [SerializeField] private AudioClip artsSE; // スキルのSE

    //弾数を増やす
    public void AddBulletCount(int value)
    {
        bulletCount += value;

        Debug.Log(
            $"<color=cyan>キャラ2 スキル弾数アップ！</color> {bulletCount}発"
        );
    }

    public bool UseSkill()
    {
        if (skillStock == null)
        {
            Debug.LogWarning("SkillStockが設定されていません");
            return false;
        }

        if (wavePrefab == null)
        {
            Debug.LogWarning("ID2のWave Prefabが設定されていません");
            return false;
        }

        if (Mouse.current == null || Camera.main == null)
            return false;

        if (!skillStock.UseStock())
            return false;

        soundPlayer.SetOneShot(artsSE);
        soundPlayer.PlayOneShot();

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


        //弾数分だけWaveを生成
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

            Vector2 waveDirection =
                Quaternion.Euler(
                    0f,
                    0f,
                    offsetAngle
                ) * direction;

            GameObject wave = Instantiate(
                wavePrefab,
                transform.position,
                Quaternion.Euler(
                    0f,
                    0f,
                    angle + offsetAngle + 90f
                )
            );

            WaterWave waterWave =
                wave.GetComponent<WaterWave>();

            if (waterWave != null)
            {
                waterWave.Setup(
                    transform,
                    waveDirection,
                    speed,
                    lifeTime,
                    waterRange
                );
            }
        }


        Debug.Log(
            "<color=blue>【ID2 スキル発動】</color>"
        );

        return true;
    }
}
