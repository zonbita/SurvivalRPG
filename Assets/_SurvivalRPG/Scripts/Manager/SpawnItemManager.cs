using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemManager : Singleton<SpawnItemManager>
{
    [SerializeField] GameObject spawnItem;


    public void SpawnAItem(ItemSO i, int quantity, Vector3 pos)
    {
        GameObject go = Instantiate(spawnItem);
        go.transform.position = pos;
        ItemDrop id = go.GetComponent<ItemDrop>();
        id.itemSO = i;
        id.quantity = quantity;
    }
}
