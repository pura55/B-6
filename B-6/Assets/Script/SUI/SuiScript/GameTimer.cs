using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    // タイマーを表示するText
    public TMP_Text timerText;

// 経過時間
private float elapsedTime = 0f;
    private static float finalElapsedTime = 0f;

    void Start()
    {
        // 討伐数をリセット
        EnemyHealth.ResetPlayerKillCount();

        // 保存されたプレイ時間をリセット
        ResetFinalElapsedTime();

        // 岩の防いだ数をリセット
        RockHit.ResetBlockedRockCount();
    }

    void Update()
    {
        // 時間を増やす
        elapsedTime += Time.deltaTime;

        // 分
        int minutes = Mathf.FloorToInt(elapsedTime / 60);

        // 秒
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        // 00:00形式で表示
        if (timerText != null) timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    /// biref@ ゲーム時間（分）を取得する関数
    public int GetGameTimeM() { return Mathf.FloorToInt(elapsedTime / 60); }

    // ゲーム時間を保存
    public void SaveElapsedTime() { finalElapsedTime = elapsedTime; }

    // 保存したゲーム時間を取得
    public static float GetFinalElapsedTime() { return finalElapsedTime; }

    // ゲーム時間をリセット
    public static void ResetFinalElapsedTime() { finalElapsedTime = 0f; }

}
