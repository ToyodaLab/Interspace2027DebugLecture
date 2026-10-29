using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    public int value = 1;

    void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.AddScore(value);
        Destroy(gameObject);
    }
}
