using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Ability")]
public class AbilityCard : Card
{

    [SerializeField] int mana;
    [SerializeField] Ability ability;
    public override void Apply(GameObject target)
    {
        ability.Apply(target);
    }

}
