using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] int startingHealth = 100;
    [SerializeField] int healthRegenPerSecond = 2;

    private float healthPoints;

    [SerializeField] UnityEvent onPlayerDeath;

    private void Awake()
    {
        healthPoints = startingHealth - 20;
    }

    private void Update()
    {
        healthPoints = Mathf.Clamp(healthPoints + (healthRegenPerSecond * Time.deltaTime), 0, startingHealth);
        if (healthPoints == 0)
        {
            DeathSequance();
        }
    }

    private void DeathSequance()
    {
        onPlayerDeath.Invoke();
        // todo particle VFX
    }

    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
    }

    public float GetHealthFraction()
    {
        return healthPoints / (float)startingHealth;
    }
}
