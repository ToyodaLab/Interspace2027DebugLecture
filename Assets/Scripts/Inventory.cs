using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [System.NonSerialized] public List<string> items;

    public void AddItem(string itemName)
    {
        items.Add(itemName);
        Debug.Log("Bought " + itemName);
    }
}
