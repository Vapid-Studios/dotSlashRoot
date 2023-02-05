using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Manager : MonoBehaviour
{
    public List<Card> cards;
    [SerializeField] List<PlayerProjectile> projectileAbility;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Card card in cards)
        {
            if (card is ProjectileCard)
            {
                ProjectileCard temp = (ProjectileCard)card;
                projectileAbility.Add(temp.projectile);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Account for input from player to spawn projectiles, do abilites etc

    }
}
