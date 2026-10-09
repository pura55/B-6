using UnityEngine;

/// <summary>
/// ボタンベース
/// 
/// ボタンの基底クラス
/// </summary>
public abstract class ButtonBase : MonoBehaviour
{
    [SerializeField] protected ButtonSoundManager buttonSoundManager; // サウンド管理

    /// @brief ボタンを押す処理を行う関数
    public virtual void OnClick()
    {
        buttonSoundManager.PlayButtonSound();
    }
}
