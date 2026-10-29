using UnityEngine;

public class FrameLogger : MonoBehaviour
{
    void Update()
    {
        Debug.Log("Frame " + Time.frameCount);
    }
}
