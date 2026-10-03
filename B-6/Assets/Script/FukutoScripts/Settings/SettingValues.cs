using UnityEngine;

/// <summary>
/// セッティングバリュー
/// 
/// 設定値を保持します
/// </summary>
[CreateAssetMenu(menuName = "Settings")]
public class SettingValues : ScriptableObject
{
    private float AUDIO_VOLUME_BGM = 1.0f; // BGM
    private float AUDIO_VOLUME_SE = 1.0f; // SE

    private bool IS_Off_AUDIO; // オーディオをオフにするフラグ
    private const float ZERO_VOLUME = 0f; // ボリュームゼロ

    /// @brief BMGのボリュームを取得する関数
    public float GetVolumeBGM()
    {
        if(!IS_Off_AUDIO)
        {
            return AUDIO_VOLUME_BGM;
        }
        else
        {
            return ZERO_VOLUME;
        }
    }

    /// @brief BGMのボリュームを設定する関数
    public void SetVolumeBGM(float bgm)
    {
        AUDIO_VOLUME_BGM = bgm;
    }

    /// @brief SEのボリュームを取得する関数
    public float GetVolumeSE()
    {   
        
        if (!IS_Off_AUDIO)
        {
            return AUDIO_VOLUME_SE;
        }
        else
        {
            return ZERO_VOLUME;
        }
    }

    /// @brief SEのボリュームを設定する関数
    public void SetVolumeSE(float se)
    {
        AUDIO_VOLUME_SE = se;
    }

    /// @brief オーディオフラグを取得する関数
    public bool GetOffAudio()
    {
        return IS_Off_AUDIO;
    }

    /// @brief オーディオフラグを設定する関数
    public void SetOffAudio(bool offAudio)
    {
        IS_Off_AUDIO = offAudio;
    }
}
