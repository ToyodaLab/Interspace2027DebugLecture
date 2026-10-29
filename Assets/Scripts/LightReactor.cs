using UnityEngine;

public class LightReactor : MonoBehaviour
{
    public Light lamp;

    public void Toggle()
    {
        lamp.enabled = !lamp.enabled;
        lamp.enabled = !lamp.enabled;
        // Hint: Flip a switch twice, what happens? / スイッチを2回切り替えると?
        Debug.Log("Lamp is now " + lamp.enabled);
    }
}
