using UnityEngine;

/// <summary>
/// ボタンベース
/// 
/// ボタンの基底クラス
/// </summary>
public abstract class ButtonBase : MonoBehaviour
{
    /// @brief ボタンを押す処理を行う関数
    public abstract void OnClick();
}
