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
            rb.AddForce(Vector3.forward * 5f, ForceMode.Impulse);
        }
    }
}
