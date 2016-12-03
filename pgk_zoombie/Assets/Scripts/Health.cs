using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
    public static int currentHealth;
    public int maxHealth;

    // Use this for initialization
    void Start()
    {
        currentHealth = 200;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if(currentHealth < 0)
        {
            currentHealth = 0;
        }
    }

    public static void addHealth(int number)
    {
        currentHealth += number;
    }

    public static void subtractHealth(int number)
    {
        currentHealth -= number;
    }
}
