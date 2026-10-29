using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [System.NonSerialized] public List<string> items;

    public void AddItem(string itemName)
    {
        // Hint: Has the list been created? Read the stack trace from the top. / リストは作られている? スタックトレースを上から読む
        items.Add(itemName);
        Debug.Log("Bought " + itemName);
    }
}
