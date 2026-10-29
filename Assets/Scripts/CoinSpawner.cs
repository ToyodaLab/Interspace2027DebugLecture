using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            Debug.Log("Spawned coin worth " + coin.GetComponent<CoinPickup>().value);
        }
    }
}
