using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats")]
public class Stats : ScriptableObject
{

    // Stat Representation
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth = 100;
    [SerializeField] int damage = 1;
    [SerializeField] float baseSpeed = 10f;
    [SerializeField] int numJumps = 1;


}
