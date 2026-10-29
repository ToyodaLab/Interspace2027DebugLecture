using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 100;
    public int armour = 20;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            int bonus = 2.5f;
            health -= CalculateDamage(30);
            Debug.Log("Health: " + health);
        }
    }

    int CalculateDamage(int attack)
    {
        int reduced = attack - armour;
        float multiplier = 1 / 2;
        return (int)(reduced * multiplier);
    }
}
