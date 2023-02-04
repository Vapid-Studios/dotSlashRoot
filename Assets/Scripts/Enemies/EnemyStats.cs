using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public float maxHealth;
    public float CurrentHealth;
    public bool CanFly;
    public bool CanJump;
    [Range(0, 10f)]
    public float MoveSpeed;
}
