using UnityEngine;

public class LivesDisplay : MonoBehaviour
{
    int startLives;

    void Awake()
    {
        // Hint: Is GameManager ready yet? See Script Execution Order. / GameManager は準備できている? 実行順を確認
        startLives = GameManager.Instance.lives;
        Debug.Log("Starting lives: " + startLives);
    }
}
