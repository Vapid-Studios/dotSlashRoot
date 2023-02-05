using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Duck : MonoBehaviour
{

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Animator animator;

    public Transform leader;
    public float followSharpness = 0.05f;
    private bool lerping;
    [SerializeField] private float TotalLerpTime;

    private Vector2 FacingDirection = Vector2.right;
    
    
    void Awake()
    {
        playerTransform = GameObject.FindWithTag("Duck Spot").transform;
    }
    
    void Update()
    {
        var a = playerTransform.position - transform.position;
        a.y = 0;
        FacingDirection = a.normalized;
        
        FlipBird();
    }

    private void LateUpdate()
    {

    }

    void FlipBird()
    {
        var scale = transform.localScale;
        
        if (FacingDirection == Vector2.right)
        {
            //multiply bird scale by -1
            scale.x = 1;
        }
        else
        {
            scale.x = -1;
        }

        transform.localScale = scale;

    }
}
