using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    protected HealthSystem healthSystem;
    protected PlayerSpriteRenderer playerSpriteRenderer;
    protected Rigidbody2D rb;
    protected Animator animator;
    protected Transform playerPos;
    protected Vector2 velocity;
    protected int damage;
    protected float speed;
    protected float animOffset;
    protected float attackDist;
    protected float attackRate;
    protected float range;
    protected string velocityX = "VelocityX";
    protected string velocityY = "VelocityY";
    protected bool facingRight;

    protected ZombieState zombieState;

    protected ZombieState State
    {
        get => zombieState;
        set
        {
            StopAllCoroutines();
            zombieState = value;

            switch (State)
            {
                case ZombieState.Patrol:
                    StartCoroutine(StatePatrol());
                    break;
                case ZombieState.Chase:
                    StartCoroutine(StateChase());
                    break;
                case ZombieState.Attack:
                    StartCoroutine(StateAttack());
                    break;
            }
        }
    }

    private void Awake()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        healthSystem = new HealthSystem(20);
        playerSpriteRenderer = new PlayerSpriteRenderer(facingRight, new List<Transform>());
        velocity = new Vector2(0f, 0f);
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.Find("Dwight").transform;
        animOffset = -0.1f;
        facingRight = true;

        State = ZombieState.Chase;
    }

    protected virtual IEnumerator StatePatrol()
    {
        while (State == ZombieState.Patrol)
        {
            velocity = Vector2.zero;

            ChangeState();
            AnimationHandler();
            
            yield return null;
        }
    }
    
    protected virtual IEnumerator StateChase()
    {
        while (State == ZombieState.Chase)
        {
            var tr = transform;
            playerPos = GameObject.Find("Dwight").transform;
            Vector3 dir = playerPos.position - tr.position;
            dir.Normalize();
            
            rb.MovePosition(tr.position + (dir * (speed * Time.deltaTime)));
            if (facingRight)
                velocity.x = speed;
            else if (!facingRight)
                velocity.x = -speed;

            if (playerPos.position.y > tr.position.y)
                velocity.y = speed;
            else if(playerPos.position.y < tr.position.y)
                velocity.y = -speed;
            
            ChangeState();
            AnimationHandler();

            yield return null;
        }
    }

    protected virtual IEnumerator StateAttack()
    {
        float nextAttack = 0f;
        velocity = Vector2.zero;

        while (State == ZombieState.Attack)
        {
            var canAttack = Time.time > nextAttack;
            speed = 0;
        
            if (canAttack)
            {
                Debug.Log("Player hit");
                nextAttack = Time.time + attackRate;
                // Player.getPlayerInstance().HealthSystem.Damage(damage);
            }
            
            ChangeState();
            AnimationHandler();

            yield return null;
        }
    }

    protected void ChangeState()
    {
        if (Vector2.Distance(transform.position, playerPos.position) > attackDist &&
            Vector2.Distance(transform.position, playerPos.position) < range &&
            State != ZombieState.Chase)
        {
            State = ZombieState.Chase;
        }
        else if (Vector2.Distance(transform.position, playerPos.position) > range && State != ZombieState.Patrol)
        {
            State = ZombieState.Patrol;
        }
        else if (Vector2.Distance(transform.position, playerPos.position) < attackDist && State != ZombieState.Attack)
        {
            State = ZombieState.Attack;
        }
        
    }

    protected void AnimationHandler()
    {
        float vertical = 0;
        animOffset = playerSpriteRenderer.GetOffset();

        if (animOffset.Equals(-0.1f)) vertical = -1f;
        else if (animOffset.Equals(0.1f)) vertical = 1f;

        if (velocity == Vector2.zero)
        {
            animator.SetFloat(velocityY, animOffset);
            playerSpriteRenderer.SortSprites(animOffset);
        }
        else
        {
            animator.SetFloat(velocityY, vertical);
            playerSpriteRenderer.SortSprites(velocity.y);
        }

        FlipTransform();
    }

    protected void FlipTransform()
    {
        Vector3 pos = transform.localPosition;
		
        if(playerPos.position.x > pos.x && !facingRight ||
           playerPos.position.x < pos.x && facingRight)				
            RotatePlayer();
    }	
    private void RotatePlayer() {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void TakeDamage(int dmg)
    {
        speed = 0;
        healthSystem.Damage(dmg);
        if (healthSystem.GetHealth() <= 0) Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
    
    public int GetHealth() => healthSystem.GetHealth();
}

public enum ZombieState
{
    Patrol,
    Chase,
    Attack
}