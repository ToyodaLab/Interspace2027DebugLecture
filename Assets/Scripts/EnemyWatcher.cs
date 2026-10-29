using UnityEngine;

public class EnemyWatcher : MonoBehaviour
{
    public GameObject enemy;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // Hint: The enemy destroys itself after 3 seconds. / 敵は 3 秒後に自分を破棄する
            Debug.Log("Enemy is at " + enemy.transform.position);
        }
    }
}
