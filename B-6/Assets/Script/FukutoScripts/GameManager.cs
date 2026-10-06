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
    [SerializeField] private PlayerSpawner playerSpawner; // プレイヤースポナー
    #endregion

    #region State
    private string mainGame = "MainGame"; // メインゲーム
    private string gameOver = "GameoverScene"; // ゲームオーバー
    private string gameClear = "GameclearScene"; // ゲームクリア―
    private string title = "TitleScene"; // タイトルシーン
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fader.SetFadeOut();
        Debug.Log("フェードアウトを命令します");
    }

    /// @brief メインゲームに遷移する関数
    public void ChangeMainGame()
    {
        fader.SetFadeIn(mainGame);
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

    /// @brief タイトルに遷移する関数
    public void ChangeTitleScene()
    {
        fader.SetFadeIn(title);
    }

    /// @brief プレイヤーのスポーンをする関数
    public void PlayerSpawn()
    {
        if(playerSpawner != null)
        {
            playerSpawner.SpawnPlayer();
        }
    }
}
