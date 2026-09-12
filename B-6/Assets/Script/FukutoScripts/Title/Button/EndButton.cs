using UnityEngine;

/// <summary>
/// エンドボタン
/// 
/// ゲーム終了ボタン
/// </summary>
public class EndButton : MonoBehaviour
{
    #region Config
    private Vector3 DefaultScreenPositon = new Vector3(0f, 1080f, 0f);
    #endregion

    #region State
    private bool isNoPressed = false; // Noボタンのフラグ
    private bool isEndPressed = false; // 終了ボタンのフラグ
    #endregion

    void Update()
    {

    }

    /// @brief Endボタンの関数
    public void EndClick()
    {
        EndGame();
    }

    public void YesClick()
    {
        EndGame();
    }

    public void NoClick()
    {

    }

    /// @brief ゲームを終了させる関数
    private void EndGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
        Application.Quit();//ゲームプレイ終了
#endif
    }
}
