using UnityEngine;

public class Countdown : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
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