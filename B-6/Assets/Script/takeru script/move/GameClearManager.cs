using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearManager : MonoBehaviour
{
    // ゲームを起動している間だけ保持
    private static int clearCount = 0;

    // 同じボスで複数回クリア判定されるのを防ぐ
    private bool isCleared = false;

    [SerializeField] private GameManager gameManager; // ゲームマネージャー

    /// <summary>
    /// ボスを倒したときに呼ぶ
    /// </summary>
    public void GameClear()
    {
        // すでにクリア済みなら何もしない
        if (isCleared)
            return;

        isCleared = true;

        // クリア回数を増やす
        clearCount++;

        Debug.Log(
            $"<color=yellow>ゲームクリア！ クリア回数 : {clearCount}</color>"
        );

        // =========================
        // クリア回数ごとの判定
        // =========================

        switch (clearCount)
        {
            case 1:
                Debug.Log("1回目クリア");
                break;

            case 2:
                Debug.Log("2回目クリア");
                break;

            case 3:
                Debug.Log("3回目クリア");
                break;

            default:
                Debug.Log($"{clearCount}回目クリア");
                break;
        }

        // ゲームクリア画面へ遷移
        gameManager.ChangeGameClear();
    }


    /// <summary>
    /// 現在のクリア回数を取得
    /// </summary>
    public static int GetClearCount()
    {
        return clearCount;
    }
}