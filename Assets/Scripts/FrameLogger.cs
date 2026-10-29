using UnityEngine;

public class FrameLogger : MonoBehaviour
{
    void Update()
    {
        // Hint: This runs every frame. Try Collapse, Clear, or disabling the component. / 毎フレーム実行されます。Collapse・Clear・無効化を試す
        if (Time.frameCount % 10 == 0)
        {
            Debug.Log("Frame " + Time.frameCount);
        }
    }
}
