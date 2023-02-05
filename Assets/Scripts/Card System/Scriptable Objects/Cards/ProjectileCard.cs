using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Projectile")]
public class ProjectileCard : Card
{

    [SerializeField] int mana;
    [SerializeField] PlayerProjectile projectile;

}
