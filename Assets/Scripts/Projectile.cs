using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int Damage;

    public float ProjectileSpeed;
    public Transform playertransform;
    private Vector3 direction;
    void Start()
    {
        playertransform = GameObject.FindWithTag("Player").transform;
        direction = (playertransform.position - transform.position).normalized;
        Debug.Log("Projectile Start");
    }
    void FixedUpdate()
    {

        transform.Translate(direction * (Time.deltaTime * ProjectileSpeed));
        
    }
}
