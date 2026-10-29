using UnityEngine;

public class Looker : MonoBehaviour
{
    public float viewDistance = 5f;

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * viewDistance, Color.red);

        // Hint: Look at the red ray and yellow Gizmo in the Scene view. / Scene ビューの赤い線と黄色い Gizmo を見る
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
