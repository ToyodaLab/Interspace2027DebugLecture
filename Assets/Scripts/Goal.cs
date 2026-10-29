using UnityEngine;

public class Goal : MonoBehaviour
{
    // Hint: Check the collider in the Inspector (Is Trigger, 2D vs 3D). / Inspector のコライダーを確認 (Is Trigger、2D と 3D)
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Goal reached by " + other.name);
    }
}
