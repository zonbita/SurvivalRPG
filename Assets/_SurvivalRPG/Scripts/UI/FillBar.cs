using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class FillBar : MonoBehaviour
{
    [SerializeField] Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetPercent(float value = 0, float maxValue = 0)
    {
        if (image == null) return;

        float percent = value / maxValue;

        if (percent == image.fillAmount) return;

         image.fillAmount = (percent);


        
    }
}
