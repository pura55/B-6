using UnityEngine;

/// <summary>
/// サウンドプレイヤー
/// 
/// SEやBGMを再生する役割を担います
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

    [Header("SETTING")]
    [SerializeField] protected SettingValues settingValues; // 設定値
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    /// @brief 通常再生する関数
    public void PlaySound()
    {
        audioSource.volume = settingValues.GetVolumeSE();
        AudioSetting(false, false);
        audioSource.Play();
    }

    /// @brief BGMを再生する関数
    public void PlayBGM()
    {
        audioSource.volume = settingValues.GetVolumeBGM();
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
        audioSource.volume = settingValues.GetVolumeSE();
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

    /// @brief ピッチを設定する関数
    public void SetAudioPitch(float pitch)
    {
        audioSource.pitch = pitch;
    }

    /// @brief 通常音を設定する関数
    public void SetNomalSound(AudioClip clip)
    {
        audioNomalSound = clip;
    }

    /// @brief BGMを設定する関数
    public void SetBGM(AudioClip clip)
    {
        audioBGM = clip;
    }

    /// @brief 効果音を設定する関数
    public void SetOneShot(AudioClip clip)
    {
        audioOneShot = clip;
    }
}
