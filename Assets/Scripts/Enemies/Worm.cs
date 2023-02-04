using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : Enemy
{

    [SerializeField] float movementSpeed = 2f;
    [SerializeField]
    private Transform rayGroundCheck, rayWallCheck;
    [SerializeField]
    float groundCheckDistance, wallCheckDistance;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] private SpriteRenderer enemySprite;
    private bool groundDetected, wallDetected;
    private int facingDirection;
    private Vector2 movement;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (Mathf.Approximately(gameObject.transform.rotation.y, 0f))
        {
            facingDirection = -1;
        }
        else
        {
            facingDirection = 1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        base.FixedUpdate();
        groundDetected = Physics2D.Raycast(rayGroundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(rayWallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }

    public override void Move()
    {

    }

    public override void Patrol()
    {

        Debug.Log("IN Worm Patrol");
        if (!groundDetected || wallDetected)
        {
            Flip();
        }
        else
        {
            // move enemy

            movement.Set(movementSpeed * facingDirection, rb.velocity.y);
            rb.velocity = movement;
            Debug.Log("Enemy Worm Move:" + rb.velocity);

        }
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }


    private void Flip()
    {


        facingDirection *= -1;
        gameObject.transform.Rotate(0f, 180f, 0f);

    }

    private void OnDrawGizmos()
    {

        // Debug Lines for to see where enemy is at in space relative to the ground and the wall
        Gizmos.DrawLine(rayGroundCheck.position, new Vector2(rayGroundCheck.position.x, rayGroundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(rayWallCheck.position, new Vector2(rayWallCheck.position.x + wallCheckDistance, rayWallCheck.position.y));
    }
}
