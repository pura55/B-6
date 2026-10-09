using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TMP_Text timerText;

    private float elapsedTime = 0f;
    private static float finalElapsedTime = 0f;
    private bool isRunning = true;

    void Start()
    {
        EnemyHealth.ResetPlayerKillCount();
        ResetFinalElapsedTime();
        RockHit.ResetBlockedRockCount();

        elapsedTime = 0f;
        isRunning = true;
    }

    void Update()
    {
        if (!isRunning) return;

        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        if (timerText != null)
        {
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    // ゲーム時間（分）を取得
    public int GetGameTimeM()
    {
        return Mathf.FloorToInt(elapsedTime / 60);
    }

    // タイマーを停止して最終時間を保存
    public void StopAndSaveTimer()
    {
        if (!isRunning) return;

        isRunning = false;
        finalElapsedTime = elapsedTime;

        Debug.Log(
            $"タイマー停止・クリア時間保存：{finalElapsedTime:F2}秒"
        );
    }

    // ゲーム時間を保存
    public void SaveElapsedTime()
    {
        finalElapsedTime = elapsedTime;
    }

    // 保存したゲーム時間を取得
    public static float GetFinalElapsedTime()
    {
        return finalElapsedTime;
    }

    // ゲーム時間をリセット
    public static void ResetFinalElapsedTime()
    {
        finalElapsedTime = 0f;
    }
}