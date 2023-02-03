using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats")]
public class Stats : ScriptableObject
{

    // Stat Representation
    public int maxHealth = 100;
    public float damage = 1f;
    public float defense = 1f;
    public float baseSpeed = 10f;
    public int numJumps = 1;

}
