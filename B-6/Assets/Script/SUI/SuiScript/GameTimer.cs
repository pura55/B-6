using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    // タイマーを表示するText
    public TMP_Text timerText;

    // 経過時間
    private float elapsedTime = 0f;

    void Update()
    {
        // 時間を増やす
        elapsedTime += Time.deltaTime;

        // 分
        int minutes = Mathf.FloorToInt(elapsedTime / 60);

        // 秒
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        // 00:00形式で表示
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}