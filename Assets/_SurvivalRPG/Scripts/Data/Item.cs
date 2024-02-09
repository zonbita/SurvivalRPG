using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string Name;
    public string Description;
    public GameObject Model;

    public abstract void Use();
}
