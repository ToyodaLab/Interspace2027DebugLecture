using UnityEngine;

public class Countdown : MonoBehaviour
{
    bool playerIsNear = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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
            // Hint: Does the loop run at all? Check its condition. / ループは実行されている? 条件を確認
            for (int i = 10; i > 10; i--)
            {
                Debug.Log("Count: " + i);
            }
            Debug.Log("Liftoff!");
        }
    }
}
