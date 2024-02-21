using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public List<TabButtons> buttons;
    TabButtons selectButton;
    public Sprite tabIdle;
    public Sprite tabActive;
    public Sprite tabHover;
    public List<GameObject> SwitchPanel;
    public void Init(TabButtons button)
    {
        if(buttons == null)
        {
            buttons = new List<TabButtons>();
        }

        //buttons.Add(button);
    }

    public void OnTabEnter(TabButtons button)
    {

        if(selectButton == null || button != selectButton)
        button.background.overrideSprite = tabHover;
    }

    public void OnTabExit(TabButtons button)
    {

    }

    public void OnTabSelect(TabButtons button)
    {
        selectButton = button;
        ResetTabs();

        
        
        int btn = button.transform.GetSiblingIndex();

        for(int j = 0; j < buttons.Count; j++)
        {
            if (j == btn) SwitchPanel[j].SetActive(true);
            else SwitchPanel[j].SetActive(false);

        }
        button.background.overrideSprite = tabActive;
    }

    public void ResetTabs()
    {
        foreach(TabButtons button in buttons)
        {
            button.background.overrideSprite = tabIdle;
        }
    }
}
