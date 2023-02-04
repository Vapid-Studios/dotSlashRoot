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
    public int numJumps = 1;

    public UnityEvent onMaxHealthChanged = new UnityEvent();
    public UnityEvent onDamageChanged = new UnityEvent();
    public UnityEvent onDefenseChanged = new UnityEvent();
    public UnityEvent onSpeedChanged = new UnityEvent();
    public UnityEvent onMaxJumpsChanged = new UnityEvent();
    
    public void TakeDamage(float damage)
    {
        if (damage < 0)
        {
            Debug.LogWarning("Cannot take negative damage; use RestoreHealth instead");
            return;
        }
    }
    
    public void RestoreHealth(float health)
    {
        if (health < 0)
        {
            Debug.LogWarning("Cannot restore negative health; use TakeDamage instead");
            return;
        }
    }

    public void AdjustMaxHealth(int health)
    {
        maxHealth += (maxHealth + health > 0) ? health : -maxHealth + 1;
    }

    public void AdjustDamage(float damage)
    {
        this.damage += (this.damage + damage > 0) ? damage : -damage + 1;
    }

    public void AdjustDefense(float defense)
    {
        this.defense += (this.defense + defense) > 1f ? defense : -defense + 1f;
    }

    public void AdjustSpeed(float speed)
    {
        baseSpeed += (baseSpeed + speed) > 5f ? speed : -baseSpeed + 5f;
    }

    public void SetMaxJumps(int maxJumps)
    {
        numJumps = maxJumps > 0 ? maxJumps : 1;
    }
}
