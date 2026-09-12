using UnityEngine;
using UnityEngine.UI;

public class MaterialGauge : MonoBehaviour
{
    public PlayerMaterial player;
    public Slider slider;

    void Update()
    {
        slider.maxValue = player.maxMaterial;
        slider.value = player.material;
    }
}