using UnityEngine;
using static UnityEditor.Progress;

[RequireComponent(typeof(CapsuleCollider))]
public  class Item : MonoBehaviour
{
    public ItemSO itemSO;

    [SerializeField] private int quantity = 0;

    public ItemSO ItemSO => itemSO;
    public int Quantity => quantity;

    CapsuleCollider capsule;

    private void Awake()
    {
        Instantiate(itemSO.Prefab, transform);
        gameObject.tag = "Item";
        capsule = GetComponent<CapsuleCollider>();
        capsule.isTrigger = true;
    }

    public Item(ItemSO item, int Quantity)
    {
        this.itemSO = item;
        this.quantity = Quantity;
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
                other.gameObject.GetComponent<InventoryManager>().Add(this.itemSO);
        }
    }
}
