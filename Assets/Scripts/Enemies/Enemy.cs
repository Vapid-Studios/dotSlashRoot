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

    private bool canJump;

    [SerializeField] private float attackRadius;
    public EnemyState state;

    [SerializeField] protected Transform playerTransform;
    public Transform GroundCheck;
    public Animator Animator;
    [SerializeField] private bool isTrackingPlayer;

    [SerializeField] protected EnemyStats stats;

    void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        if (isTrackingPlayer)
        {
            if (PlayerInRange())
            {
                state = EnemyState.Attack;
            }
            else
            {
                state = EnemyState.Move;
            }

            return;
        }

        if (PlayerInSight()) // player is not tracking but is in sight
        {
            isTrackingPlayer = true;
            state = PlayerInRange() ? EnemyState.Attack : EnemyState.Move;
            return;
        }

        state = EnemyState.Patrol;

    }

    public void FixedUpdate()
    {
        switch (state)
        {
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Patrol:
                Patrol();
                break;
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

        if (!GetComponent<Renderer>().isVisible) return false;

        if (Vector2.Distance(playerTransform.position, transform.position) < stats.SightRange) return true;

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
