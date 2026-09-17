using UnityEngine;

/// <summary>
/// スクリーンベース
/// 
/// スクリーンの基底クラス
/// </summary>
public abstract class ScreenBase : MonoBehaviour
{
    #region Config
    protected Vector3 startPosition = new Vector3(0f, 0f, 0f); // 開始位置
    protected Vector3 endPosition = new Vector3(0f, 0f, 0f); // 戻るボタンが押された後の位置
    protected float changeSpeed = 5000f; // スクリーン変更スピード
    protected RectTransform myRectTransform; 
    #endregion
}
