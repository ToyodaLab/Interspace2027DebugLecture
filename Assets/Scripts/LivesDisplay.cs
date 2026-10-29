using UnityEngine;

public class LivesDisplay : MonoBehaviour
{
    int startLives;

    void Awake()
    {
        startLives = GameManager.Instance.lives;
        Debug.Log("Starting lives: " + startLives);
    }
}
