using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Projectile")]
public class ProjectileCard : Card
{

    [SerializeField] float damage;
    [SerializeField] int mana;
    [SerializeField] public PlayerProjectile projectile;
    PlayerProjectile SpawnProjectile(Vector2 pos)
    {
        PlayerProjectile p = Instantiate(projectile, pos, Quaternion.identity);
        return p;
    }

}
