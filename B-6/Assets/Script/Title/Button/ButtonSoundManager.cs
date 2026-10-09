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
    [SerializeField] private AudioClip buttonPush; // ボタンを押したときのサウンド
    [SerializeField] private AudioClip closePush; // クローズを押したときのサウンド

    /// @brief ボタンのSEを再生する関数
    public void PlayButtonSound()
    {
        soundPlayer.SetOneShot(buttonPush);
        soundPlayer.SetAudioPitch(buttunPitch);
        soundPlayer.PlayAtPoint();
    }

    /// @brief クローズのSEを再生する関数
    public void PlayCloseSound()
    {
        soundPlayer.SetOneShot(closePush);
        soundPlayer.SetAudioPitch(buttunPitch);
        soundPlayer.PlayAtPoint();
    }
}
