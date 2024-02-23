using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public  class ItemDrop : MonoBehaviour, IAction
{
    public ItemSO itemSO;
    public ItemSO ItemSO => itemSO;
    [Min(1)][SerializeField] private int quantity;
    //public int Quantity => quantity;

    GameObject prefab;
    CapsuleCollider capsule;
    NameHud_UI UI;

    EPlayerAction playerAction = EPlayerAction.PickUP;

    private void Awake()
    {
        prefab = Instantiate(itemSO.Prefab, transform);
        gameObject.tag = "Item";
        gameObject.name = itemSO.Name;

        capsule = GetComponent<CapsuleCollider>();
        UI = GetComponent<NameHud_UI>();
        
        capsule.isTrigger = true;
        playerAction = itemSO.playerAction;

        //quantity = quantity > itemSO.MaxStack ? itemSO.MaxStack : quantity;

        SetNameAndColor();
    }

    public ItemDrop(ItemSO item, int Quantity)
    {
        this.itemSO = item;
        this.quantity = Quantity > item.MaxStack ? item.MaxStack : Quantity;
        this.playerAction = item.playerAction;
    }

    void SetNameAndColor()
    {
        string color = "white";

        switch (ItemSO.Rarity)
        {
            case EItemRarity.Gold:
                color = "yellow";
                break;
            case EItemRarity.Uncommon:
                color = "white";
                break;
            case EItemRarity.Common:
                color = "aqua";
                break;
            case EItemRarity.Rare:
                color = "Orange";
                break;
            case EItemRarity.Epic:
                color = "purple";
                break;
            case EItemRarity.Legendary:
                color = "red";
                break;
        }
        string format;

        if (itemSO.Category == EItemCategory.Consumable || itemSO.Category == EItemCategory.Material || itemSO.Category == EItemCategory.Resource || itemSO.Category == EItemCategory.Gold)
        {
            format = string.Format("<color={0}>{1}</color> <color=white>x{2}</color>", color, ItemSO.Name, quantity);
        }
        else
        {
            format = string.Format("<color={0}>{1}</color>", color, ItemSO.Name);
        }
        UI.Set(format);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameObject.SetActive(false);

            if (itemSO.ItemID == EItemID.Gold)
            {
                if (quantity < 1)
                {
                    GameManager.Instance.TotalCoin++;
                }
                else
                {
                    GameManager.Instance.TotalCoin += quantity;
                }
                    
            }
            else 
                other.gameObject.GetComponent<InventoryManager>().Add(this.itemSO, quantity);
        }
    }

    public EPlayerAction GetPlayerAction()
    {
        return playerAction;
    }
    private void OnDestroy()
    {
        if(prefab) Destroy(prefab);
    }
}
