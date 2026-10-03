using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// チェンジボリューム
/// 
/// ボリュームを変更します
/// </summary>
public class ChangeVolume : SettingSliderBase
{
    [SerializeField] private bool isBGM = false;
    [SerializeField] private bool isSE = false;

    private void Start()
    {
        if(isBGM)
        {
            mySlider.value = settingValues.GetVolumeBGM();
        }
        else if(isSE)
        {
            mySlider.value = settingValues.GetVolumeSE();
        }
    }

    /// @brief ボリュームをスライドする関数
    public override void ChangeSlide()
    {
        base.ChangeSlide();
        if (isBGM)
        {
            ChangeBGM();
        }
        else if(isSE)
        {
            ChangeSE();
        }
    }

    /// @brief BGMを変更する関数
    private void ChangeBGM()
    {
        settingValues.SetVolumeBGM(mySlider.value);
    }

    /// @brief SEを変更する関数
    private void ChangeSE()
    {
        settingValues.SetVolumeSE(mySlider.value);
    }
}