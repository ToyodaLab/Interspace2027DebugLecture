using UnityEngine;

public class Shop : MonoBehaviour
{
    public Inventory inventory;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventory.AddItem("Sword");
        }
    }
}
