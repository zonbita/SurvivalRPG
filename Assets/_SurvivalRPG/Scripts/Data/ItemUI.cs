using TMPro;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    TMP_Text text;

    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
    }

    public ItemUI(string description)
    {
        text.text = description;
    }
}
