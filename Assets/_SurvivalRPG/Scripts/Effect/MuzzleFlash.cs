using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    [SerializeField] private Transform muzzleFlash;
    [SerializeField] private GameObject flashLight;
    [SerializeField] private float duration;

    private void OnEnable() => Hide();

    public void Show() => StartCoroutine(ShowMuzzleFlash());

    private void RotateMuzzle()
    {
        int angle = Random.Range(0, 360);
        muzzleFlash.localRotation = Quaternion.Euler(muzzleFlash.localEulerAngles.x, 0, angle);
    }
    
    IEnumerator ShowMuzzleFlash()
    {
        if (!muzzleFlash.gameObject.activeSelf) yield return null;

        muzzleFlash.gameObject.SetActive(true);
        flashLight.SetActive(true);
        RotateMuzzle();

        yield return new WaitForSeconds(duration);
        Hide();
    }

    private void Hide()
    {
        muzzleFlash.gameObject.SetActive(false);
        flashLight.SetActive(false);
    }
}
