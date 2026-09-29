using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// チェンジエクスプラネーション
/// 
/// 説明を変更するクラス
/// </summary>
public class ChangeExplanation : MonoBehaviour
{
    #region State
    private Image explanationImage; // キャラの画像
    private Sprite[] explanationSprites; // 説明スプライトの配列
    private string explanationPass = "title/GUI/character_explanation_"; // 説明画像のパス
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadExplanationSprites();

        explanationImage = GetComponent<Image>();
    }

    /// @brief 説明スプライトをロードする関数
    private void LoadExplanationSprites()
    {
        // 配列のサイズを取得
        explanationSprites = new Sprite[4];

        // 順番に配列に代入
        for (int i = 0; i < 4; i++)
        {
            int id = i + 1;

            Sprite sprite = Resources.Load<Sprite>(explanationPass + id);

            explanationSprites[i] = sprite;

            if (explanationSprites[i] == null) Debug.LogError("説明画像が読み込まれていません！");
        }
    }

    /// @brief 説明スプライトを変更するする関数
    /// @param element 要素数
    public void ChangeSprite(int element)
    {
        explanationImage.sprite = explanationSprites[element];
    }
}
