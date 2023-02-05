using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grimace : Enemy
{

    [SerializeField] private Transform feetTransform;

    private Vector2 StartingDirection = Vector2.left;

    public LayerMask LayerMask;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        FlipCharacter();
    }

    void FixedUpdate()
    {
        base.FixedUpdate();
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
        Debug.Log("Grimace Attack");
    }

    void FlipCharacter()
    {
        if (StartingDirection == Vector2.left)
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
