using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class CraftManager : MonoBehaviour
{
    List<ItemSO> items = new List<ItemSO>();
    [SerializeField] GameObject Panel;
    [SerializeField] GameObject CraftingSlot_UI;
    [SerializeField] GameObject InfoPanel;

    private void Start()
    {
        LoadAllCraft();
        GeneralCraftUI();
    }

    void LoadAllCraft()
    {
        string directoryPath = "Assets/_SurvivalRPG/SO"; // Relative path to the directory

        // Check 
        if (Directory.Exists(directoryPath))
        {
            // Get all 
            string[] files = Directory.GetFiles(directoryPath, "*.asset", SearchOption.AllDirectories);
            foreach (string filePath in files)
            {
                string Path = filePath.Replace(Application.dataPath, "Assets");
                ItemSO itemSO = AssetDatabase.LoadAssetAtPath<ItemSO>(Path);
                if (itemSO != null && itemSO.Blueprint.RequiredItems != null && itemSO.Blueprint.RequiredItems.Count != 0)
                {
                    items.Add(itemSO);
                }
            }
        }
    }

    public void GeneralCraftUI()
    {
        if (Panel == null) return;

        for (int i = 0; i < items.Count; i++)
        {
            Crafting_Slot_UI ui = Instantiate(CraftingSlot_UI, Panel.transform).GetComponent<Crafting_Slot_UI>();
            ui.Init(i, items[i].Icon);
            ui.OnClick += OnClick;
        }

    }

    public bool CheckCraft(ItemSO item)
    {
        foreach(RequiredItem r in item.Blueprint.RequiredItems)
        {
            //PlayerInventory.Instance.
        }
        return false;
    }

    private void OnClick(int slot)
    {
        if (InfoPanel == null) return;

        InfoPanel.GetComponent<ItemDescription_UI>()?.Set(items[slot]);
    }
}
