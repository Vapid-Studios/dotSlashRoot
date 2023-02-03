using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Duck : MonoBehaviour
{

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Animator animator;

    private bool lerping;
    [SerializeField] private float TotalLerpTime;
    private bool flipped;
    
    void Awake()
    {
        playerTransform = GameObject.FindWithTag("Duck Spot").transform;
    }
    
    void Update()
    {
        if (Vector3.Distance(playerTransform.position, transform.position) > 10 )
        {
            if(!lerping)
                StartCoroutine(Lerp());
        }
        else
        {
            lerping = false;
            StopAllCoroutines();
        }

        animator.SetBool("Moving", lerping);

        if (playerTransform.position.x - transform.position.x < 0)
        {
            FlipBird(true);
        }
        else
        {
            FlipBird(false);
        }
    }

    private IEnumerator Lerp()
    {
        var time = 0.0f;
        lerping = true;

        while (time <= TotalLerpTime)
        {
            time += Time.deltaTime;
            float interpolationRatio = time / TotalLerpTime;

            transform.position = Vector2.Lerp(transform.position, playerTransform.position, interpolationRatio);
            yield return new WaitForEndOfFrame();
        }
    }

    void FlipBird(bool negative)
    {
        var scale = transform.localScale;
        if (negative)
        {
            //multiply bird scale by -1
            scale.x *= -1;
        }
        else
        {
            if (flipped)
            {
                //multiply bird scale by -1
                scale.x *= -1;
            }
        }

        transform.localScale = scale;

    }
}
