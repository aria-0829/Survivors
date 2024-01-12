using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    // Events
    public event EventHandler OnDamage;
    public event EventHandler OnHeal;
    public event EventHandler OnDeath;

    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Image bar;

    private float currentHealth;
    private bool isDead;

    public float CurrentHealth { get { return currentHealth; } }
    public float CurrentHealthNormalized { get { return currentHealth / maxHealth; } } // Used for applying 0-1 ranges
    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }

    private void Awake()
    {
        currentHealth = maxHealth;
        if (bar == null)
        {
            bar = GetComponentsInChildren<Image>()[1];
            bar.fillAmount = CurrentHealthNormalized;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (bar != null) bar.fillAmount = CurrentHealthNormalized;

        if (currentHealth < 0)
        {
            currentHealth = 0;
            Die();
            OnDeath?.Invoke(this, EventArgs.Empty);
        }

        OnDamage?.Invoke(this, EventArgs.Empty); // ? is checking if OnDamage is null
    }

    public void Heal(float heal)
    {
        currentHealth += heal;
        if (bar != null) bar.fillAmount = CurrentHealthNormalized;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        OnHeal?.Invoke(this, EventArgs.Empty); 
    }

    public void Die()
    {
        isDead = true;
    }

    public void Setup()
    {
        currentHealth = maxHealth;
    }

    public bool IsDead()
    {
        return isDead;
    }

    public bool IsHurt()
    {
        if (currentHealth < maxHealth)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
