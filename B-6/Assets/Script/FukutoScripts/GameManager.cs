using UnityEngine;

/// <summary>
/// ゲームマネージャー
/// 
/// ゲームを管理するのに関連したコードを記述します
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Config
    [SerializeField] private Fader fader; // フェイダー
    #endregion

    #region State
    private string gameOver = "GameoverScene"; // ゲームオーバー
    private string gameClear = "GameclearScene"; // ゲームクリア―
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fader.SetFadeOut();
        Debug.Log("フェードアウトを命令します");
    }

    /// @brief ゲームオーバーに遷移する関数
    public void ChangeGameOver()
    {
        fader.SetFadeIn(gameOver);
    }

    /// @brief ゲームクリア―に遷移する関数
    public void ChangeGameClear()
    {
        fader.SetFadeIn(gameClear);
    }
}
