using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, ITakeDamage, IMove, IPatrol, IAttack
{
    private bool isGrounded;

    private bool canJump;

    [SerializeField] private float attackRadius;


    [SerializeField] private Transform playerTransform;
    public Transform GroundCheck;
    public Animator Animator;



    void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInSight())
        {
            if (PlayerInRange())
            {
                Attack();
            }
            else
            {
                Move();
            }
        }
        else
        {
            Patrol();
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(damage);
    }

    public abstract void Move();


    private bool PlayerInSight()
    {
        //Todo: logic for if enemy should start attacking player

        if (GetComponent<Renderer>().isVisible) return true;
        return false;
    }

    public abstract void Patrol();

    public abstract void Attack();

    private bool PlayerInRange()
    {
        //Todo: logic for if player is within attacking distance
        if (Vector2.Distance(playerTransform.position, transform.position) < attackRadius) return true;
        return false;
    }
}
