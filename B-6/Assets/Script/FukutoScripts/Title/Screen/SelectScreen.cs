using UnityEngine;

/// <summary>
/// セレクトスクリーン
/// 
/// キャラクター選択スクリーンクラス
/// </summary>
public class SelectScreen : StartScreen
{
    void Start()
    {
        myRectTransform = GetComponent<RectTransform>();
        startPosition = new Vector3(3000f, 0f, 0f);
        endPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        PressPlayButton();

        PressBackButton();
    }
}
