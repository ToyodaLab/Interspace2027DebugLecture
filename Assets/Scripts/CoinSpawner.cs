using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            // Hint: The prefab and the scene instance can be different. / プレハブとシーン上のものは違うことがある
            Debug.Log("Spawned coin worth " + coin.GetComponent<CoinPickup>().value);
        }
    }
}
