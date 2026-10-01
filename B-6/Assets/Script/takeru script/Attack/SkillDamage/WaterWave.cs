using UnityEngine;
using System.Collections.Generic;

public class WaterWave : MonoBehaviour
{
    [Header("ダメージ")]
    [SerializeField] public int damage = 1;

    [Header("DATA")]
    [SerializeField]
    private PlayerProgressData playerProgressData;

    private Vector2 direction;

    private float speed;
    private float lifeTime;
    private float waterRange;

    private float spawnTime;

    // 波を出した場所
    private Vector3 startPosition;

    // 波に当たって押されている敵
    private List<Transform> pushedEnemies =
        new List<Transform>();


    public void Setup(
        Transform playerTransform,
        Vector2 moveDirection,
        float moveSpeed,
        float extinction,
        float range
    )
    {
        direction =
            moveDirection.normalized;

        speed = moveSpeed;
        lifeTime = extinction;
        waterRange = range;

        spawnTime = Time.time;

        // 波を出した位置を保存
        startPosition = transform.position;

        // プレイヤーデータからスキル攻撃力取得
        if (playerProgressData != null)
        {
            damage =
                playerProgressData.skillDmg;
        }
    }


    private void Update()
    {
        // =========================
        // 今フレームの移動量
        // =========================

        Vector3 moveAmount =
            (Vector3)direction
            * speed
            * Time.deltaTime;


        // =========================
        // 波を移動
        // =========================

        transform.position +=
            moveAmount;


        // =========================
        // 当たった敵も一緒に押す
        // =========================

        for (int i = pushedEnemies.Count - 1;
             i >= 0;
             i--)
        {
            // 敵が倒されて消えていた場合
            if (pushedEnemies[i] == null)
            {
                pushedEnemies.RemoveAt(i);
                continue;
            }

            // 波と同じ方向・同じ速度で押す
            pushedEnemies[i].position +=
                moveAmount;
        }


        // =========================
        // 時間で消滅
        // =========================

        if (Time.time - spawnTime >= lifeTime)
        {
            Destroy(gameObject);
            return;
        }


        // =========================
        // 発動地点から一定距離で消滅
        // =========================

        float distance =
            Vector2.Distance(
                startPosition,
                transform.position
            );

        if (distance >= waterRange)
        {
            Destroy(gameObject);
            return;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // =========================
        // 壁
        // =========================

        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
            return;
        }


        // =========================
        // 敵
        // =========================

        if (other.CompareTag("Enemy"))
        {
            // -------------------------
            // ダメージ
            // -------------------------

            EnemyHealth health =
                other.GetComponent<EnemyHealth>();

            if (health != null)
            {
                health.ReceiveDamage(
                    damage
                );
            }


            // -------------------------
            // 押し流す対象に追加
            // -------------------------

            if (!pushedEnemies.Contains(
                other.transform
            ))
            {
                pushedEnemies.Add(
                    other.transform
                );
            }
        }
    }
}