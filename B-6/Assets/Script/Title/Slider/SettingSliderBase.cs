using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// セッティングスライダーベース
/// 
/// 設定スライダーのベース
/// </summary>
public class SettingSliderBase : MonoBehaviour
{
    [SerializeField] protected SettingValues settingValues; //設定値
    [SerializeField] protected Slider mySlider; // スライダー

    ///　@brief スライダーを変更時に処理される関数
    public virtual void ChangeSlide()
    {
    }
}
