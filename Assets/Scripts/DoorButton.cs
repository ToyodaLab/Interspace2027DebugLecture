using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public GameObject door;

    void OnTriggerEnter(Collider other)
    {
        door.SetActive(false);
    }
