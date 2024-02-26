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

    public void AddList(List<Attribute> array)
    {
        foreach(Attribute a in array)
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

    public float Get_A_Attribute(EAttribute a)
    {
        if (!attributes.ContainsKey(a)) return 0;
        if (attributes.TryGetValue(a, out float totalValue))
        {
            return totalValue; // Return 
        }
        else
        {
            return 0; // Return 0
        }
    }

    public void Add_A_AttributeTotal(AttributeTotal a)
    {
        foreach(var e in a.attributes)
        {
            if (attributes.ContainsKey(e.Key))
                attributes[e.Key] += e.Value;
            else
                attributes[e.Key] = e.Value;
        }

    }

    public void SetNew(AttributeTotal attributeTotal)
    {
        attributes.Clear();

        foreach (var kvp in attributeTotal.attributes)
        {
            attributes[kvp.Key] = kvp.Value;
        }
    }

    public void Clear()
    {
        attributes.Clear();
        attributes = new();
    }

}