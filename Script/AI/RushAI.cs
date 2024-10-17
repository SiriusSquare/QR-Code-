using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushAI : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    public float acceleration = 2f;
    public float deceleration = 2f;
    private SpriteRenderer spriteRenderer;
    private Vector3 velocity = Vector3.zero; 
    private Animator animator;
    public float Animationspeed = 1f;
    void Start()
    {
        if (animator != null)
        {
            animator = GetComponent<Animator>();
            animator.speed = Animationspeed;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            
            if (spriteRenderer != null)
            {
                if (player.position.x < transform.position.x)
                {
                    spriteRenderer.flipX = true;
                }
                else
                {
                    spriteRenderer.flipX = false;
                }
            }
            
            Vector3 direction = (player.position - transform.position).normalized;

            
            velocity = Vector3.Lerp(velocity, direction * moveSpeed, acceleration * Time.deltaTime);

            
            transform.position += velocity * Time.deltaTime;
        }
    }
}
