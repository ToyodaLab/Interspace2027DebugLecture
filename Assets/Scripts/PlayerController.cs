using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Hint: Is this value really 5 in the Inspector? / Inspector の値は本当に 5?
    public float speed = 5f;
    public float jumpForce = 5f;
    Rigidbody rb;

    void Start()
    {
        // Hint: Is this the same rb as the field above? / 上のフィールドの rb と同じ?
        Rigidbody rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Move the player
        float h = Input.GetAxis("Horizontal");
        // Hint: The Console error points at the end of the next line. / 次の行の終わりにエラーがあります
        float v = Input.GetAxis("Vertical")
        // Hint: How long is one frame? / 1フレームの長さは?
        transform.Translate(h * speed, 0, v * speed);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
