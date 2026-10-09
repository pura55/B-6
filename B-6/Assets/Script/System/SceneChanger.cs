using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンチェンジャー
/// 
/// シーン変更を行います
/// </summary>
public class SceneChanger : MonoBehaviour
{
    /// @brief シーンを変更する関数
    public void SceneChange(string sceneName)
    {
        // シーン遷移
        SceneManager.LoadScene(sceneName);
        return;
    }
}
