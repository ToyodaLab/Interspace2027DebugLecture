using UnityEngine;

public class LightReactor : MonoBehaviour
{
    public Light lamp;

    public void Toggle()
    {
        lamp.enabled = !lamp.enabled;
        lamp.enabled = !lamp.enabled;
        Debug.Log("Lamp is now " + lamp.enabled);
    }
}
