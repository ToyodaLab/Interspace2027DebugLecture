using UnityEngine;

public class Spinner : MonoBehaviour
{
    public float degreesPerSecond = 90f;

    // Hint: Check the spelling and capital letters. / つづりと大文字小文字を確認
    void update()
    {
        transform.Rotate(0, degreesPerSecond * Time.deltaTime, 0);
    }
}
