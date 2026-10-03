
using UnityEngine;
using System.Collections;

public class RockHit : MonoBehaviour
{
    #region Config
    [SerializeField] private SoundPlayer soundPlayer; // サウンドプレイヤー
    [SerializeField] private int hitDamage = 50; // ヒット時のダメージ
    [SerializeField] private float waitTime = 0.6f; // 破壊待機時間
    [SerializeField] private float playPtich = 1.5f; // 破壊音再生速度
    #endregion

    #region State
    private bool isHit = false; // ヒットフラグ
    private static int blockedRockCount = 0; // 防いだ落石の数
    #endregion

    // 防いだ落石の数を取得
    public static int GetBlockedRockCount()
    {
        return blockedRockCount;
    }

    // 防いだ落石の数をリセット
    public static void ResetBlockedRockCount()
    {
        blockedRockCount = 0;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (isHit)
        {
            return;
        }

        HitTower(collision);

        HitPlayer(collision);

        HitWall(collision);
    }

    /// @brief 防衛対象への衝突判定を行う関数
    private void HitTower(Collision2D collision)
    {
        // 防衛対象に触れた
        if (collision.gameObject.CompareTag("Tower"))
        {
            PlayDestroySE();
            TowerHealth towerHealth = collision.gameObject.GetComponent<TowerHealth>();

            if (towerHealth != null)
            {
                towerHealth.TakeDamage(hitDamage);
            }

            isHit = true;

            StartCoroutine(WaitFinisheSound());
            return;
        }
    }

    /// @brief プレイヤーへの衝突判定を行う関数
    private void HitPlayer(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayDestroySE();
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.ReceiveDamage(hitDamage);
            }

            isHit = true;

            StartCoroutine(WaitFinisheSound());
            return;
        }
    }

    /// @brief 壁への衝突判定を行う関数
    private void HitWall(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // 防いだ落石をカウント
            blockedRockCount++;
            Debug.Log("防いだ落石：" + blockedRockCount);

            PlayDestroySE();
            // 壁破壊
            Destroy(collision.gameObject);

            isHit = true;

            StartCoroutine(WaitFinisheSound());
            return;
        }
    }

    /// @brief サウンド停止待機を行う関数
    IEnumerator WaitFinisheSound()
    {
        yield return new WaitForSeconds(waitTime);

        Destroy(gameObject);
    }

    private void PlayDestroySE()
    {
        soundPlayer.StopSounds();
        soundPlayer.SetAudioPitch(playPtich);
        soundPlayer.PlayOneShot();
    }
}