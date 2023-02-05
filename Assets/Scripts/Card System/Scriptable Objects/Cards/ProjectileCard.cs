using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Projectile")]
public class ProjectileCard : Card
{

    [SerializeField] int damage;
    [SerializeField] int mana;
    [SerializeField] public PlayerProjectile projectile;
    [SerializeField] Vector2 offset;
    public PlayerProjectile SpawnProjectile(Vector2 pos)
    {
        PlayerProjectile p = Instantiate(projectile, pos + offset, Quaternion.identity);
        p.SetDamage(damage);
        return p;
    }

}
