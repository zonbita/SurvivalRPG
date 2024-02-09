using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLifeDisable : MonoBehaviour
{
    public float LifeTime = 2f;

    private void OnEnable()
    {
        StartCoroutine(DisableAfterDelay(LifeTime));
    }
    private void Start()
    {
        StartCoroutine(DisableAfterDelay(LifeTime));
    }

    private IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Disable the GameObject
        gameObject.SetActive(false);
    }
}
