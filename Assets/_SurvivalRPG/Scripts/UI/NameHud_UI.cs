using UnityEngine;
using UnityEngine.UI;

public class NameHud_UI : MonoBehaviour
{
    [SerializeField] public GameObject UI;
    Text text;
    void Awake()
    {
        text = GetComponentInChildren<Text>();
    }

    public void Set(string msg)
    {
        text.text = msg;
    }

    private void OnBecameVisible()
    {
        if (gameObject.activeSelf)
        {
            UI.SetActive(true);
        }
    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf)
        {
            UI.SetActive(false);
        }
    }

}
