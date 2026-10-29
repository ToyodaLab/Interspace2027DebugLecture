using UnityEngine;

public class DoorButton : MonoBehaviour
{
    // Hint: Is this slot filled in the Inspector? Also count the { and }. / Inspector で設定済み? { と } の数も確認
    public GameObject door;

    void OnTriggerEnter(Collider other)
    {
        door.SetActive(false);
    }
