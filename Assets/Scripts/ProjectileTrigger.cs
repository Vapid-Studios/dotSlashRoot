using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileTrigger : MonoBehaviour
{

    public UnityEvent<int> OnProjectileEnterColliderEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            OnProjectileEnterColliderEvent.Invoke(other.gameObject.GetComponent<Projectile>().Damage);
        }
    }
}
