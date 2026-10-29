using UnityEngine;

public class Looker : MonoBehaviour
{
    public float viewDistance = 5f;

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * viewDistance, Color.red);

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, viewDistance))
        {
            Debug.Log("I can see " + hit.collider.name, this);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);
    }
}
