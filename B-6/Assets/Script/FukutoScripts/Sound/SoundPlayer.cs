using UnityEngine;

/// <summary>
/// サウンドプレイヤー
/// 
/// SEを再生する役割を担います
/// </summary>
public class SoundPlayer : MonoBehaviour
{
    #region Config
    [Header("CLIPS")]
    [SerializeField] protected AudioClip audioNomalSound; // 通常音
    [SerializeField] protected AudioClip audioBGM; // BGM
    [SerializeField] protected AudioClip audioOneShot; // 効果音

    [Header("SOUCE")]
    [SerializeField] protected AudioSource audioSource; // ソースのコンポーネント
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    /// @brief 通常再生する関数
    public void PlaySound()
    {
        AudioSetting(false, false);
        audioSource.Play();
    }

    /// @brief BGMを再生する関数
    public void PlayBGM()
    {
        AudioSetting(true, false);
        audioSource.Play();
    }

    /// @brief 再生を停止する関数
    public void StopSounds()
    {
        audioSource.Stop();
    }

    /// @brief 一度だけ再生する関数
    public void PlayOneShot()
    {
        AudioSetting(false, true);
        audioSource.PlayOneShot(audioOneShot);
    }

    /// @brief 音の設定をする関数
    protected void AudioSetting(bool isBGM, bool isShot)
    {
        if(isBGM)
        {
            audioSource.clip = audioBGM;
            audioSource.loop = true;
            return;
        }
        else if(isShot) 
        {
            audioSource.loop = false;
            return;
        }
        else
        {
            audioSource.clip = audioNomalSound;
            audioSource.loop = false;
            return;
        }
    }

    public void SetAudioPitch(float pitch)
    {
        audioSource.pitch = pitch;
    }
}
