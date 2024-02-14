using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fading : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _fadeSpeed;

    public void StartFading()
    {
        _image.color = Color.white;
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        while (_image.color.a > 0)
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _image.color.a - _fadeSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
