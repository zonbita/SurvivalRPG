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

    public void SetPercent(float value, float maxValue)
    {
        if (image == null) return;

        float percent = Mathf.Clamp01( value / maxValue);

        if (percent == image.fillAmount) return;

         image.fillAmount = percent;
    }
}
