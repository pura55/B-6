using UnityEngine;

/// <summary>
/// ボタンサウンドマネジャー
/// 
/// ボタンの再生する音を管理します
/// </summary>
public class ButtonSoundManager : MonoBehaviour
{
    [SerializeField] private SoundPlayer soundPlayer; // サウンドプレイヤー
    [SerializeField] private float buttunPitch = 1.0f; // ボタンを押したときのピッチ

    /// @brief ボタンのSEを再生する関数
    public void PlayButtonSound()
    {
        soundPlayer.SetAudioPitch(buttunPitch);
        soundPlayer.PlayAtPoint();
    }
}
