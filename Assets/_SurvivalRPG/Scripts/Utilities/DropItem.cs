using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] List<DropItemInfo> items = new();
    public float dropRadius = 3;
    GameObject prefab;
    void Awake()
    {
        prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_SurvivalRPG/Prefabs/Item/Item.prefab");
    }

    public void Drop()
    {
        if (items.Count > 0 && prefab)
        {
            int num = Random.Range(0, items.Count);
            Vector3 randomDirection = Random.insideUnitSphere * dropRadius;
            randomDirection.y = 0;
            GameObject go = Instantiate(prefab, this.transform.position + randomDirection, Quaternion.identity) as GameObject;
            ItemDrop itemDrop = go.GetComponent<ItemDrop>();
            itemDrop.itemSO = items[num].ItemSO;
            itemDrop.quantity = items[num].Quantity;
            
        }
    }
    private void OnDisable()
    {
        Drop();
    }

}

[System.Serializable]
public class DropItemInfo
{
    public ItemSO ItemSO;
    public int Quantity = 1;
}