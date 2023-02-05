using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponMelee : MonoBehaviour
{
    [SerializeField] private AudioSource hitAudioSource;

    public UnityEvent onDamage = new UnityEvent();

    private void OnTriggerEnter2D(Collider2D collider)
    {
        hitAudioSource.Play();
        if (collider.gameObject.CompareTag("CanDamage") && !collider.gameObject.CompareTag("Player"))
        {
            if (collider.gameObject.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.TakeDamage(3);
            }
        }

        onDamage?.Invoke();
    }
}
