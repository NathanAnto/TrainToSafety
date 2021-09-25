using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    protected HealthSystem healthSystem;
    protected MovementSystem movementSystem;
    protected Rigidbody2D rb;
    protected Animator animator;
    protected Transform playerPos;
    protected float speed;
    private int damage;
    private float attackDist = 5f;
    
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

    private void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        healthSystem = new HealthSystem(20);
        movementSystem = new MovementSystem(new List<Transform>());
        State = ZombieState.Chase;
        rb = GetComponent<Rigidbody2D>();
    }

    private IEnumerator StatePatrol()
    {
        while (State == ZombieState.Patrol)
        {
            if (Vector2.Distance(transform.position, playerPos.position) > attackDist)
                State = ZombieState.Chase;
                
            yield return null;
        }        
    }
    
    private IEnumerator StateChase()
    {
        while (State == ZombieState.Chase)
        {
            if (Vector2.Distance(transform.position, playerPos.position) > attackDist)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
                Debug.Log("Moving zombie");
            }
            else
                State = ZombieState.Attack;

            yield return null;
        } 
    }
    
    private IEnumerator StateAttack()
    {
        Player.getPlayerInstance().HealthSystem.Damage(damage);
        yield return null;
    }

    public void TakeDamage(int dmg)
    {
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