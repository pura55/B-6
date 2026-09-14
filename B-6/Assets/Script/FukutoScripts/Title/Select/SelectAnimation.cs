using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// セレクトアニメーション
/// 
/// 選択画面のアニメーション（キャラクター）
/// </summary>
public class SelectAnimation : MonoBehaviour
{

    // キャラのスプライト
    [System.Serializable]
    public class CharacterSprites
    {
        public Sprite[] idleSprites; // キャラスプライトの配列
        public int size;
    }

    #region State
    private string spritePlayerBasePass = "Player/ID_"; // プレイヤーの基本スプライトパス
    private string spriteIdlePass = "/Idle"; // 待機パス

    private const int maxID = 3; // IDの最大値
    private int characterID = 0; // CharacterSpriteの配列を管理するために使用する為、最小値は0

    private Vector3 secondCharacterPosition = new Vector3(0f, 50f, 0f); // 2キャラ目の座標
    private Vector3 secondCharacterScale = new Vector3(2.5f, 2.5f, 1f); // 2キャラ目のスケール
    private Vector3 otherCharacterScale = new Vector3(4f, 4f, 1f); // それ以外のキャラクタースケール

    private float currentTime = 0f; // 現在のフレーム(スプライト)の時間
    private const float timePerSprite = 0.1f; // 1スプライト当たりの時間
    private int spriteIndex = 0; // スプライトの指数

    private Image image; // 画像
    private RectTransform rectTransform; // レクトトランスフォーム
    public CharacterSprites[] charactersSprites; // キャラクタースプライトの配列
    #endregion

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        LoadSprite();
    }

    void Update()
    {
        IdleAnimation();
    }

    /// @brief 使用するスプライトをロードする関数
    private void LoadSprite()
    {
        charactersSprites = new CharacterSprites[4];

        for (int i = 0; i < 4; i++)
        {
            // クラスとして新しく実体を作る
            CharacterSprites characterSprites = new CharacterSprites();

            int id = i + 1;

            characterSprites.idleSprites = Resources.LoadAll<Sprite>(spritePlayerBasePass + id + spriteIdlePass);
            characterSprites.size = characterSprites.idleSprites.Length;

            charactersSprites[i] = characterSprites;

            if (charactersSprites[i] == null) Debug.LogError("キャラクタースプライトの実体がありません！");
        }
    }

    /// @brief 待機アニメーションを行う関数
    private  void IdleAnimation()
    {
        ManageFrame(charactersSprites[characterID].size);
        image.sprite = charactersSprites[characterID].idleSprites[spriteIndex];
    }

    /// @brief アニメーションのフレーム管理を行う関数
    private void ManageFrame(int elements)
    {
        // 現在の時間がスプライトごとの時間よりも小さい場合
        if (currentTime < timePerSprite)
        {
            currentTime += Time.deltaTime; // 時間を進める
        }
        else
        {

            // 指数 + 1 が要素以上だったら指数を戻す
            if (elements <= (spriteIndex + 1))
            {
                spriteIndex = 0;
            }
            else
            {
                // スプライト指数を進める
                spriteIndex++;
            }

            // フレームをリセット
            currentTime = 0f;
        }
    }

    /// @brief スプライトを次に進める関数
    public void NextToSprites()
    {
        characterID ++;
        spriteIndex = 0; // 範囲外参照を防ぐために0に設定
        currentTime = 0f;

        // 上限を設定
        if (maxID < characterID) characterID = maxID;

        // トランスフォームを変更
        ChangeTransform();
    }

    /// @brief スプライトを前に戻す関数
    public void BackToSprites()
    {
        characterID--;
        spriteIndex = 0; // 範囲外参照を防ぐために0に設定
        currentTime = 0f;

        // 下限を設定
        if (characterID < 0) characterID = 0;

        // トランスフォームを変更
        ChangeTransform();
    }

    /// @brief トランスフォームを変更する関数
    private void ChangeTransform()
    {
        // 2キャラ目が大きいためスケール変更
        if (characterID != 1)
        {
            rectTransform.localScale = otherCharacterScale;
            rectTransform.localPosition = Vector3.zero;
        }
        else
        {
            rectTransform.localScale = secondCharacterScale;
            rectTransform.localPosition = secondCharacterPosition;
        }
    }

    /// @brief キャラクターのIDを取得する関数
    public int GetCharacterID()
    {
        return characterID + 1;
    }
}
