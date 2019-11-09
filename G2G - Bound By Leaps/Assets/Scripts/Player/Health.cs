using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int startingHealth = 100;
    [SerializeField] int healthRegenPerSecond = 2;

    private int healthInt;
    private float healthFloat;

    private void Awake()
    {
        healthFloat = startingHealth - 20;
    }

    private void Update()
    {
        healthFloat = Mathf.Clamp(healthFloat + (healthRegenPerSecond * Time.deltaTime), 0, startingHealth);
        healthInt = Mathf.RoundToInt(healthFloat);
    }

    public void TakeDamage(int damage)
    {
        healthFloat -= damage;
        //print("damaga " + damage + "health" + healthFloat);
    }

    public float GetHealthPrecent()
    {
        return healthFloat / (float)startingHealth;
    }
}
