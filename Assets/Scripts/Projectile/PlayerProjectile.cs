using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] int damage;

    void Start()
    {
        rb.velocity = Vector2.right * projectileSpeed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Colliding with valid enemy that can be damaged
        if (col.gameObject.CompareTag("CanDamage"))
        {
            if (col.gameObject.TryGetComponent<Enemy>(out var enemy))
            {
                Debug.Log("Hit Enemy");
                enemy.TakeDamage(damage);
            }
        }

        if (!col.gameObject.GetComponent<PlayerMovement>())
            Destroy(gameObject);
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

}
