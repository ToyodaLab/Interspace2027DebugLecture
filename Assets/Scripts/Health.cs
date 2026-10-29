using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 100;
    public int armour = 20;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            // Hint: Can a float fit in an int? / float を int に入れられる?
            int bonus = 2.5f;
            health -= CalculateDamage(30);
            Debug.Log("Health: " + health);
        }
    }

    int CalculateDamage(int attack)
    {
        int reduced = attack - armour;
        // Hint: What is 1 / 2 when both are ints? / int 同士の 1 / 2 は?
        float multiplier = 1 / 2;
        return (int)(reduced * multiplier);
    }
}
