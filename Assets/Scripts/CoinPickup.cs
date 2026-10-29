using UnityEngine;

// Hint: Compare the class name with the file name. / クラス名とファイル名を比べる
public class CoinPickUp : MonoBehaviour
{
    public int value = 1;

    void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.AddScore(value);
        Destroy(gameObject);
    }
}
