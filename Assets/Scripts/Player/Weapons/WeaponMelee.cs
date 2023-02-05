using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMelee : MonoBehaviour, IAttack
{
    [SerializeField] private AudioSource hitAudioSource;
    private BoxCollider2D bc2d;
    private void Start()
    {
        bc2d = gameObject.GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        hitAudioSource.Play();
        if (collision.collider.gameObject.CompareTag("CanDamage"))
        {
            if (collision.collider.gameObject.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.TakeDamage(10);
            }
        }
    }

    public void Attack(GameObject target)
    {
        
    }
}
