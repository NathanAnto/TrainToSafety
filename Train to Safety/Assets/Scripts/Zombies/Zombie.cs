using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private HealthSystem healthSystem;
    private PlayerSpriteRenderer playerSpriteRenderer;
    protected Rigidbody2D rb;
    private Animator animator;
    protected Transform playerPos;
    private ZombieChaseBehaviour chaseBehaviour;
    private ZombiePatrolBehaviour patrolBehaviour;
    private ZombieAttackBehaviour attackBehaviour;
    protected Vector2 velocity;
    protected int damage;
    protected float speed;
    private float animOffset;
    protected float attackDist;
    protected float attackRate;
    protected float range;
    protected string velocityX = "VelocityX";
    private string velocityY = "VelocityY";
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
        patrolBehaviour = new ZombiePatrolBehaviour();
        chaseBehaviour = new ZombieChaseBehaviour();
        attackBehaviour = new ZombieAttackBehaviour();
        
        animator = transform.GetChild(0).GetComponent<Animator>();
        healthSystem = new HealthSystem(20);
        playerSpriteRenderer = new PlayerSpriteRenderer(facingRight);
        velocity = new Vector2(0f, 0f);
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.Find("Dwight").transform;
        animOffset = -0.1f;
        facingRight = true;

        State = ZombieState.Chase;
    }

    protected virtual IEnumerator StatePatrol()
    {
        Debug.Log("Patrol");
        yield return patrolBehaviour.DoBehaviour();
    }
    
    protected virtual IEnumerator StateChase()
    {
        Debug.Log("Chase");
        yield return chaseBehaviour.DoBehaviour();
    }

    protected virtual IEnumerator StateAttack()
    {
        Debug.Log("Attack");
        yield return attackBehaviour.DoBehaviour();
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

        if (velocity == Vector2.zero) {
            animator.SetFloat(velocityY, animOffset);
            playerSpriteRenderer.SortSprites(animOffset);
        } else {
            animator.SetFloat(velocityY, vertical);
            playerSpriteRenderer.SortSprites(velocity.y);
        }

        FlipTransform();
    }

    protected void FlipTransform() {
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

    private void Die() => Destroy(gameObject);
    public int GetHealth() => healthSystem.GetHealth();
}

public enum ZombieState
{
    Patrol,
    Chase,
    Attack
}