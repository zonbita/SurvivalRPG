using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Attribute
{
    public EAttribute Name;
    public float Value;

    public void Add(float Value) { this.Value += Value; }

    public void Sub(float Value) { this.Value -= Value;}

    public void Default() { this.Value = 0; }
}

public class AttributeTotal
{
    public Dictionary<EAttribute, float> attributes = new Dictionary<EAttribute, float>();

    public void Add(Attribute a) 
    {
        if(attributes.ContainsKey(a.Name))
            attributes[a.Name] += a.Value;
        else
            attributes[a.Name] = a.Value;
    }

    public float Get_A_AttributeTotal(Attribute a)
    {
        if (!attributes.ContainsKey(a.Name)) return 0;

        if (attributes.TryGetValue(a.Name, out float totalValue))
        {
            return totalValue; // Return the total value if found
        }
        else
        {
            return 0; // Return 0 if the attribute is not found
        }
    }
}