using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Stat")]
public class StatsCard : Card
{
    public StatsEffect statsEffect;
    public override void Apply(GameObject target)
    {
        statsEffect.Apply(target);
    }

}
