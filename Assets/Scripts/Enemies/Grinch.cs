using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class Grinch : Enemy
{
    private float startingX;
    [SerializeField] [Range(0, 10)] private float MoveRange;

    public LayerMask mask;
    private int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        startingX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Move()
    {
        var direction = (playerTransform.position - transform.position).normalized;
        var playerDirection = new Vector2(direction.x, 0).normalized;
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Vector2.Distance(transform.position, playerTransform.position) + 1, mask ) ;
        Debug.DrawRay(transform.position, direction, Color.green);
        
        if (hit.collider.gameObject.tag == "Player")
        {
            transform.Translate(direction * (stats.MoveSpeed * Time.fixedDeltaTime));
        }
        else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("ForeGround"))
        {
            hit = Physics2D.Raycast(transform.position, playerDirection, Math.Abs(transform.position.x - playerTransform.position.x), mask);
            if (hit.collider == null)
            {
                transform.Translate(Vector3.left * (stats.MoveSpeed * Time.fixedDeltaTime));
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("ForeGround"))
            {
                Vector2 topOfCamera = Camera.main.ScreenToWorldPoint(new Vector2(transform.position.x, Screen.height));
                hit = Physics2D.Raycast(topOfCamera, playerDirection, Math.Abs(transform.position.x - playerTransform.position.x), mask);
                if (hit.collider == null)
                {
                    transform.Translate(Vector2.up * (stats.MoveSpeed * Time.fixedDeltaTime));
                }
            }
        }
    }

    public override void Patrol()
    {
        if (transform.position.x < startingX || transform.position.x > startingX + MoveRange)
        {
            direction *= -1;
        }
        transform.Translate(Vector2.right * (stats.MoveSpeed * Time.fixedDeltaTime * direction));
    }


    public void OnDrawGizmos()
    {
       // Gizmos.DrawLine(transform.position, playerTransform.position);
    }
    
    public override void Attack()
    {
        throw new System.NotImplementedException();
    }
}
