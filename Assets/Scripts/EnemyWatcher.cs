using UnityEngine;

public class EnemyWatcher : MonoBehaviour
{
    public GameObject enemy;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Enemy is at " + enemy.transform.position);
        }
    }
}
