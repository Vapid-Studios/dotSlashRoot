using System.Security.Cryptography;
using UnityEngine;
using Debug = UnityEngine.Debug;


public enum EnemyState
{
    Attack,
    Move,
    Patrol,
    TakeDamage
}

public abstract class Enemy : MonoBehaviour, ITakeDamage, IMove, IPatrol, IAttack
{
    private bool isGrounded;
    private Renderer renderer;
    private bool canJump;

    [SerializeField] private float attackRadius;
    public EnemyState state;

    [SerializeField] protected Transform playerTransform;
    public Transform GroundCheck;
    public Animator Animator;
    [SerializeField] private GameObject currentTarget;
    [SerializeField] protected EnemyStats stats;

    void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        renderer = GetComponent<Renderer>();
        if (!renderer)
            renderer = GetComponentInChildren<Renderer>();
        if (!stats)
            stats = GetComponent<EnemyStats>();
    }

    // Start is called before the first frame update
    public void Start()
    {
        stats.CurrentHealth = stats.maxHealth;
    }

    // Update is called once per frame
    public void Update()
    {

        if (PlayerInSight()) // player is not tracking but is in sight
        {
            state = PlayerInRange() ? (EnemyState.Attack) : EnemyState.Move;
            if (PlayerInRange())
            {
                currentTarget = playerTransform.gameObject;
                state = EnemyState.Attack;
            }
            else
            {
                currentTarget = null;
                state = EnemyState.Move;
            }
            return;
        }

        // Player not in sight and is not being tracked
        state = EnemyState.Patrol;

    }

    public void FixedUpdate()
    {
        switch (state)
        {
            case EnemyState.Attack:
                Attack(currentTarget);
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Patrol:
                Patrol();
                break;
        }
    }

    public void TakeDamage(int damage)
    {
        stats.CurrentHealth -= damage;
        
        if(stats.CurrentHealth <= 0)
            Destroy(gameObject);
    }

    public abstract void Move();


    private bool PlayerInSight()
    {
        //Todo: logic for if enemy should start attacking player

        if (!renderer.isVisible) return false;

        if (Vector2.Distance(playerTransform.position, transform.position) < stats.SightRange) return true;

        return false;
    }

    public abstract void Patrol();

    public abstract void Attack(GameObject target);

    private bool PlayerInRange()
    {
        //Todo: logic for if player is within attacking distance
        if (Vector2.Distance(playerTransform.position, transform.position) < attackRadius) return true;
        return false;
    }
}
