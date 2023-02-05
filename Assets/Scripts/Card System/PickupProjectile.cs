using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupProjectile : MonoBehaviour
{
    [SerializeField] ProjectileCard assignedCard;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            var card_manager = FindObjectOfType<Card_Manager>();
            card_manager.AddToProjectileAbilites(assignedCard);
            Destroy(gameObject);
        }
    }
}
