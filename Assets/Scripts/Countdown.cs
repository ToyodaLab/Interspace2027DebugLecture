using UnityEngine;

public class Countdown : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            int count = 10;
            while (count > 0)
            {
                Debug.Log("Count: " + count);
            }
        }
    }
}
