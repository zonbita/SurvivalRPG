
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescription_UI : MonoBehaviour
{
    [SerializeField] Image imageItem;
    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text Destext;
    [SerializeField] FillBar fillBar;

    [SerializeField] GameObject RequirementList;
    public void Set(ItemSO item)
    {
        imageItem.sprite = item.Icon;
        imageItem.enabled = true;
        text.text = $"<color={GlobalVar.GetRarityColor(item.Rarity)}>{item.Name}</color>" + "\nCrafting Time: " + item.Duration + "\n" + "Category: " + item.Category;
        Destext.text = SetDescription(item);
        SetRequirementSlots(item);
    }

    public string SetDescription(ItemSO item)
    {
        string format = item.Description
            +  "\n\n" 
            + "<color=#87CEFA>ATTRIBUTE</color>" + "\n"
            ;

        foreach (Attribute a in item.Attributes)
        {
            format += string.Format("  <color={0}>{1}:</color> <color=white>{2}</color>\n", GlobalVar.GetAttributeColor(a.Name), a.Name, a.Value);
        }
        return format;
    }

    public void SetRequirementSlots(ItemSO item)
    {
        if(RequirementList.transform.childCount < 1) return;

        DisableAllChild();

        for (int i=0; i < item.Blueprint.RequiredItems.Count; i++)
        {
            GameObject go = RequirementList.transform.GetChild(i).gameObject;
            if (go) 
            {
                go.SetActive(true);
                Transform c;
                c = go.transform.Find("Item");
                c.GetComponent<Image>().sprite = item.Blueprint.RequiredItems[i].item.Icon;
                c = go.transform.Find("Text");
                c.GetComponent<TMP_Text>().text = item.Blueprint.RequiredItems[i].item.Name + "\nx" + item.Blueprint.RequiredItems[i].Quantity.ToString();
            }
           
        }
    }

    void DisableAllChild()
    {
        for (int i = 0; i < RequirementList.transform.childCount ; i++)
        {
            RequirementList.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
