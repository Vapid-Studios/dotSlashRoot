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
    [SerializeField] private GameObject attackCloud;
    private bool groundDetected, wallDetected;
    private int facingDirection;
    private Vector2 movement;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
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
        groundDetected = Physics2D.Raycast(rayGroundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(rayWallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }

    void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Move()
    {
        // Worm is approaching Player

        // Get the current direction of this enemy relative to the player
        var x = playerTransform.position - transform.position;
        x.y = 0;
        Vector2 current_Direction = x.normalized;

        // if the player is currently in a different direction than the enemy's facing direction, than rotate the enemy accordingly
        if (current_Direction.x != facingDirection)
            gameObject.transform.Rotate(0f, 180f, 0f);
        // Adjust the facingDirection to correspond with our currentDirection
        facingDirection = (int)current_Direction.x;
        movement.Set(base.stats.MoveSpeed * facingDirection, 0f);
        rb.velocity = movement;
        Debug.Log(rb.velocity);
    }

    public override void Patrol()
    {

        ResetToIdleIfAttacking();
        // Debug.Log("IN Worm Patrol");
        if (!groundDetected || wallDetected)
        {
            Flip();
        }
        else
        {
            // move enemy

            movement.Set(base.stats.MoveSpeed * facingDirection, 0f);
            rb.velocity = movement;
            // Debug.Log("Enemy Worm Move:" + rb.velocity);

        }
    }

    public override void Attack(GameObject target)
    {

        // Stop Enemy
        movement.Set(0f, 0f);
        rb.velocity = movement;

        // Player Layer
        if (target.layer == 8)
        {
            if (!isAnimationPlaying(base.Animator, "attack"))
            {
                base.Animator.SetTrigger("Attacking");
            }

        }
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        // player is the selected target
        MonoBehaviour[] targets = collision.gameObject.GetComponents<MonoBehaviour>();
        if (collision.gameObject.layer == 8)
        {
            Debug.Log("Player has collided with " + gameObject.name);
            foreach (MonoBehaviour target in targets)
            {
                if (target is ITakeDamage)
                {
                    ITakeDamage damageable = (ITakeDamage)target;
                    damageable.TakeDamage(10);
                    Debug.Log("Player is taking Damage");
                }
            }



        }

    }

    bool isAnimationPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }

    void ResetToIdleIfAttacking()
    {
        if (isAnimationPlaying(base.Animator, "attack"))
        {

        }
    }
}
