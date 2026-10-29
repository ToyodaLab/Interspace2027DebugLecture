using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    int[] scores = { 10, 20, 30 };

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            int total = 0;
            for (int i = 0; i <= scores.Length; i++)
            {
                total += scores[i];
            }
            Debug.Log("Total: " + total);
        }
    }
}
