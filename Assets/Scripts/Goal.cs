using UnityEngine;

public class Goal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Goal reached by " + other.name);
    }
}
