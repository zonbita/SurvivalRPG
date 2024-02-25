using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class InventorySlotChild_UI : MonoBehaviour
{
    Image image;
    TMP_Text text;
    int slot = -1;
    public EEquipType equipType;
    ItemSO itemSO;

    private void Awake()
    {
        image = GetComponentInChildren<Image>();
        text = GetComponentInChildren<TMP_Text>();
    }

    public void Set(ItemSO i)
    {

            itemSO = i;
        
    }

    public void SetEmpty()
    {
        slot = -1;
        itemSO = null;
    }
}
