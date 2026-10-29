using UnityEngine;

public class Enemy : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 3f);
    }
}
