using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WormHealthBar : MonoBehaviour
{
    public Animator HealthBarAnimator;

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.gameObject.CompareTag("Player"))
        {
            HealthBarAnimator.SetTrigger("UIOpen");
        }
    }
}
