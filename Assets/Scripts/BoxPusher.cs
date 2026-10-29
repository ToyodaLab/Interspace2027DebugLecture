using UnityEngine;

public class BoxPusher : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            // Hint: Does this GameObject have a Rigidbody? Same message as the Player, different cause. / この GameObject に Rigidbody はある? Player と同じメッセージで原因は別
            rb.AddForce(Vector3.forward * 5f, ForceMode.Impulse);
        }
    }
}
