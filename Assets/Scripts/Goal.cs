using UnityEngine;

public class Goal : MonoBehaviour
{
    // Hint: Check the collider in the Inspector (Is Trigger). / Inspector のコライダーを確認 (Is Trigger)
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Goal reached by " + other.name);
    }
}
