using UnityEngine;

public class Countdown : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            int count = 10;
            // Hint: Does anything change inside the loop? Save first! / ループ内で何か変わる? 先に保存!
            while (count > 0)
            {
                Debug.Log("Count: " + count);
            }
        }
    }
}
