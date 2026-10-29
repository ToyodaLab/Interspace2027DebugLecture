using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    int[] scores = { 10, 20, 30 };

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            int total = 0;
            // Hint: Valid indexes run from 0 to Length - 1. / 有効な添字は 0 から Length - 1
            for (int i = 0; i <= scores.Length; i++)
            {
                total += scores[i];
            }
            Debug.Log("Total: " + total);
        }
    }
}
