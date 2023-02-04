using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Stats")]
public class Stats : ScriptableObject, ITakeDamage
{

    // Stat Representation
    public int maxHealth = 100;
    public float damage = 1f;
    public float defense = 1f;
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

    public void TakeDamage(float damage)
    {
        if (damage < 0)
        {
            Debug.LogWarning("Cannot take negative damage; use RestoreHealth instead");
            return;
        }
        
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

    public void AdjustDamage(float damage)
    {
        this.damage += (this.damage + damage >= 1) ? damage : -damage + 1;
        
        onDamageChanged?.Invoke();
    }

    public void AdjustDefense(float defense)
    {
        this.defense += (this.defense + defense) >= 1f ? defense : -defense + 1f;
        
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
