using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Text))]
public class InventorySlotChild_UI : MonoBehaviour
{
    Image image;
    TMP_Text text;
    int slot = -1;
    EEquipType equipType;
    ItemSO itemSO;

    private void Awake()
    {
        image = GetComponent<Image>();
        text = GetComponent<TMP_Text>();
    }

    public void Set(ItemSO i)
    {
        if(i.EquipType != equipType)
        {
            itemSO = i;
        }
    }

    public void SetEmpty()
    {
        slot = -1;
        itemSO = null;
    }
}
