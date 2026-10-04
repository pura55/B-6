using UnityEngine;
using System.Collections;

/// <summary>
/// エクスプラネーションボタンマネージャー
/// 
/// ゲーム説明で使用するボタンを管理します
/// </summary>
public class ExplanationButtonManager : MonoBehaviour
{
    #region Config
    [SerializeField] private ExplanationManager explanationManager; // ゲーム説明マネージャー
    [SerializeField] private GameObject nextButton; // 次へボタン
    [SerializeField] private GameObject frontButton; // 前へボタン
    [SerializeField] private float waitTime = 0.9f; // 待機時間
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    /// @brief ボタンを管理する関数
    public void ManageButton()
    {
        ManageNextButton();
        ManageFrontButton();
    }

    /// @brief 次へボタンを管理する関数
    private void ManageNextButton()
    {
        // 
        if(explanationManager.GetCurrentElement() == explanationManager.GetMaxElement())
        {
            nextButton.SetActive(false);
        }
        else
        {
            nextButton.SetActive(true);
        }
    }

    /// @brief 前へボタンを管理する関数
    private void ManageFrontButton()
    {
        int minElement = 0; // 最小値を判定するため0を代入

        if(explanationManager.GetCurrentElement() == minElement)
        {
            frontButton.SetActive(false);
        }
        else
        {
            frontButton.SetActive(true);
        }
    }
}
