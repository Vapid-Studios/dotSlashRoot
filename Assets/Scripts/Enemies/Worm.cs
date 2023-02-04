using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : Enemy
{

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
        facingDirection = 1;

    }

    // Update is called once per frame
    void Update()
    {
        groundDetected = Physics2D.Raycast(rayGroundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(rayWallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }

    public override void Move()
    {

    }

    public override void Patrol()
    {
        if (!groundDetected || wallDetected)
        {
            Flip();
        }
        else
        {
            // move enemy
            movement.Set(movementSpeed * facingDirection, rb.velocity.y);
            rb.velocity = movement;

        }
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }


    private void Flip()
    {

        facingDirection *= -1;
        enemySprite.transform.Rotate(0f, 180f, 0f);

    }
}
