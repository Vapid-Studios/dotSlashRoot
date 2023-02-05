using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Stats")]
public class Stats : ScriptableObject, ITakeDamage
{
    public Sprite hat;
    // Stat Representation
    public int maxHealth = 6;
    public int currentHealth;
    public int damage = 1;
    public int defense = 1;
    public float baseSpeed = 10f;
    public float attackSpeed = 1f;
    
    public int numJumps = 1;
    public int jumpsUsed = 0;

    public UnityEvent onMaxHealthChanged = new UnityEvent();
    public UnityEvent onDamageChanged = new UnityEvent();
    public UnityEvent onDefenseChanged = new UnityEvent();
    public UnityEvent onSpeedChanged = new UnityEvent();
    public UnityEvent onAttackSpeedChanged = new UnityEvent();
    public UnityEvent onMaxJumpsChanged = new UnityEvent();
    
    public UnityEvent onDamageTaken = new UnityEvent();
    public UnityEvent onHealthRestored = new UnityEvent();


    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            Debug.LogWarning("Cannot take negative damage; use RestoreHealth instead");
            return;
        }

        currentHealth -= damage;
        
        onDamageTaken?.Invoke();
    }
    
    public void RestoreHealth(float health)
    {
        if (health < 0)
        {
            Debug.LogWarning("Cannot restore negative health; use TakeDamage instead");
            return;
        }
        
        onHealthRestored?.Invoke();
    }

    public void AdjustMaxHealth(int health)
    {
        maxHealth += (maxHealth + health >= 1) ? health : -maxHealth + 1;
        
        onMaxHealthChanged?.Invoke();
    }

    public void AdjustDamage(int damage)
    {
        this.damage += (this.damage + damage >= 1) ? damage : -damage + 1;
        
        onDamageChanged?.Invoke();
    }

    public void AdjustDefense(int defense)
    {
        this.defense += (this.defense + defense) >= 1f ? defense : -defense + 1;
        
        onDefenseChanged?.Invoke();
    }

    public void AdjustSpeed(float speed)
    {
        baseSpeed += (baseSpeed + speed) >= 5f ? speed : -baseSpeed + 5f;
        
        onSpeedChanged?.Invoke();
    }
    
    public void AdjustAttackSpeed(float speed)
    {
        attackSpeed += (attackSpeed + speed >= 1) ? speed : -attackSpeed + 1;
    
        onAttackSpeedChanged?.Invoke();
    }

    public void SetMaxJumps(int maxJumps)
    {
        numJumps = maxJumps > 0 ? maxJumps : 1;
        
        onMaxJumpsChanged?.Invoke();
    }
}
