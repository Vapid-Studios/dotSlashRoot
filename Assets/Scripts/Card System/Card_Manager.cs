using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Manager : MonoBehaviour
{
    public List<Card> cards;
    [SerializeField] List<ProjectileCard> projectileAbilities;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        foreach (Card card in cards)
        {
            if (card is ProjectileCard)
            {
                ProjectileCard temp = (ProjectileCard)card;
                projectileAbilities.Add(temp);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        // Fireball
        if (Input.GetMouseButtonDown(1))
        {
            projectileAbilities[0].SpawnProjectile(playerTransform.position);
        }
        if (Input.GetKeyDown("t"))
        {
            projectileAbilities[1].SpawnProjectile(playerTransform.position);
        }

    }

    public bool AddToProjectileAbilites(ProjectileCard toAdd)
    {
        if (!cards.Contains(toAdd))
        {
            cards.Add(toAdd);
            projectileAbilities.Add(toAdd);
            return true;
        }
        return false;
    }
}
