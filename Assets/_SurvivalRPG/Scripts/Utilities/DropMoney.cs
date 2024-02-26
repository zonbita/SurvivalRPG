using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

public class DropMoney : MonoBehaviour
{
    public int amount;
    public float dropRadius = 3;
    GameObject prefab;
    void Awake()
    {
        prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_SurvivalRPG/Prefabs/Item/Gold.prefab");
    }

    public void Drop()
    {
        if(amount != 0 && prefab != null)
        {
            Vector3 randomDirection = Random.insideUnitSphere * dropRadius;
            randomDirection.y = 0;
            GameObject go = Instantiate(prefab, this.transform.position + randomDirection, Quaternion.identity);
            go.GetComponent<ItemDrop>().quantity = amount;
        }
    }
    private void OnDisable()
    {
        Drop();
    }
}
