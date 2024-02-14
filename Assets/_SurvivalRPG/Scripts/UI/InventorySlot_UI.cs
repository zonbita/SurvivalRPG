using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent (typeof(Text))]
public class InventorySlot_UI : MonoBehaviour
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

    public void Init(int Slot = -1)
    {
        slot = Slot;

    }

    public void Set(int Slot, ItemSO i)
    {
        slot = Slot;
        itemSO = i;
    }
}
