using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CockRoach : Enemy
{

    [SerializeField] private Transform feetTransform;

    private Vector2 StartingDirection = Vector2.right;

    public LayerMask LayerMask;

    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        base.FixedUpdate();
    }

    void Update()
    {
        base.Update();
        FlipCharacter();
    }
    public override void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position,
            stats.MoveSpeed * Time.fixedDeltaTime);

        var x = playerTransform.position - transform.position;
        x.y = 0;
        StartingDirection = x.normalized;
    }

    public override void Patrol()
    {
        RaycastHit2D hit = Physics2D.Raycast(feetTransform.position, StartingDirection, 10, LayerMask);

        if (hit.collider)
        {
            var a = hit.transform.GetComponent<Tilemap>().WorldToCell(hit.point);
            var b = feetTransform.position;
            if (Vector2.Distance(new Vector2(a.x, a.y), b) < 1f)
                StartingDirection *= -1;
        }

        transform.Translate(StartingDirection * (stats.MoveSpeed * Time.fixedDeltaTime));
    }

    public override void Attack(GameObject target)
    {
        Debug.Log("CockRoach Attack");
        var halfwayVector = (StartingDirection + Vector2.up).normalized;
        rb.AddForce(halfwayVector * 5);
    }

    void FlipCharacter()
    {
        if (StartingDirection == Vector2.right)
        {
            var scale = transform.localScale;
            scale.x = 1;
            transform.localScale = scale;
        }
        else
        {
            var scale = transform.localScale;
            scale.x = -1;
            transform.localScale = scale;
        }
    }
}
