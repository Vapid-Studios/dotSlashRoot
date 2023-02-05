using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    [Range(0, 1000)] public int maxHealth;
    public int CurrentHealth;
    public bool CanFly;
    public bool CanJump;
    [Range(1, 10)] public float SightRange;
    [Range(0, 10f)]
    public float MoveSpeed;
    public int damage;

}
