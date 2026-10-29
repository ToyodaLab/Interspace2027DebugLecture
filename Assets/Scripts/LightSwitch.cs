using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public LightReactor reactor;
    bool playerIsNear = false;

    void OnTriggerEnter(Collider other)
    {
        // Hint: Tags are case-sensitive. Also check the collider and the reactor slot. / タグは大文字小文字を区別。コライダーと reactor も確認
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
