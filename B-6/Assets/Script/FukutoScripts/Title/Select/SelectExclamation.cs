using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedExclamation : MonoBehaviour
{
    #region Config
    private float decreaseVelocity = 0.4f;
    #endregion

    #region State
    private Image myImage;
    private Image childImage;
    private TextMeshProUGUI childText;
    private Color childColor;
    #endregion

    void Start()
    {
        myImage = GetComponent<Image>();
        childImage = transform.GetChild(0).GetComponent<Image>();
        childText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        myImage.color = new Color(1, 1, 1, 0);
        childImage.color = myImage.color;
        childText.color = myImage.color;
    }

    void Update()
    {
        if((myImage.color.a) <= 0)
        {
            return;
        }

        DecreaseAlpha();
    }

    private void DecreaseAlpha()
    {
        float alpha = myImage.color.a;

        alpha -= decreaseVelocity * Time.deltaTime;

        myImage.color = new Color(1, 1, 1, alpha);
        childImage.color = myImage.color;
        childText.color = myImage.color;
    }

    public void SetAlpha()
    {
        myImage.color = new Color(1, 1, 1, 1);
    }
}
