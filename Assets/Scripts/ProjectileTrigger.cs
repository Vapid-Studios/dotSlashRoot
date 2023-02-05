using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileTrigger : MonoBehaviour
{

    [Serializable]
    public class OnProjectileEnterColliderEvent : UnityEvent<int>
    {
        
    }

    public OnProjectileEnterColliderEvent ProjectileEntered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            ProjectileEntered.Invoke(other.gameObject.GetComponent<Projectile>().Damage);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.name == "HurtBox")
        {
            ProjectileEntered.Invoke(1);
        }
    }
}
