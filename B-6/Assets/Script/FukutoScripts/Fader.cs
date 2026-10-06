using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// フェイダー
/// 
/// 演出上のフェイドインフェイドアウトを行います
/// </summary>
public class Fader : MonoBehaviour
{
    #region Config
    [SerializeField] private Image fade; // 影
    [SerializeField] private SceneChanger sceneChanger; // シーン変更
    [SerializeField] private float changeVelocity = 0.4f; // 変更速度
    #endregion

    #region State
    private const float maxAlpha = 1.0f; // α値の最大
    private const float minAlpha = 0f; // α値の最小
    private Vector3 fadeColor = Vector3.zero; // 影の色
    private bool isFadeIn = false; // フェードインのフラグ
    private bool isFadeOut = false; // フェードアウトのフラグ
    private string sceneName = string.Empty; // シーンの名前
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fade.color = new Color(fadeColor.x, fadeColor.y, fadeColor.z, minAlpha);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isFadeIn && !isFadeOut)
        {
            return;
        }

        if(fade != null)
        {
            if(isFadeIn)
            {
                FadeIn();
                return;
            }

            if(isFadeOut)
            {
                FadeOut();
                return;
            }
        }
    }

    /// @brief フェードインを行う関数
    private void FadeIn()
    {
        // α値取得
        float alpha = fade.color.a;

        // 時間経過で減少
        alpha += changeVelocity * Time.deltaTime;

        fade.color = new Color(fadeColor.x, fadeColor.y, fadeColor.z, alpha);

        // 最大値以上になったらシーン遷移
        if (maxAlpha <= alpha)
        {
            isFadeIn = false;

            if(sceneName == string.Empty)
            {
                return;
            }
            sceneChanger.SceneChange(sceneName);
            return;
        }
    }

    /// @brief フェードアウトを行う関数
    private void FadeOut()
    {
        // α値取得
        float alpha = fade.color.a;

        // 時間経過で減少
        alpha -= changeVelocity * Time.deltaTime;

        fade.color = new Color(fadeColor.x, fadeColor.y, fadeColor.z, alpha);

        // 最小値以下になったらフェードアウト終了
        if (alpha <= minAlpha)
        {
            isFadeOut = false;
            fade.enabled = false;
            return;
        }
    }

    /// @brief フェードインを設定する関数
    public void SetFadeIn(string name)
    {
        isFadeIn = true;
        fade.color = new Color(fadeColor.x, fadeColor.y, fadeColor.z, minAlpha);
        sceneName = name;
        fade.enabled = true;
    }

    /// @brief フェードアウトを設定する関数
    public void SetFadeOut()
    {
        isFadeOut = true;
        fade.color = new Color(fadeColor.x, fadeColor.y, fadeColor.z, maxAlpha);
        fade.enabled = true;
    }
}
