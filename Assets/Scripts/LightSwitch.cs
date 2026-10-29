using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public LightReactor reactor;
    bool playerIsNear = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            playerIsNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }

    void Update()
    {
        if (playerIsNear && Input.GetKeyDown(KeyCode.E))
        {
            reactor.Toggle();
        }
    }
}
