using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public Transform playerPos;
    protected HealthSystem healthSystem;
    protected int damage;
    protected float speed;
    protected float attackDist = 10f;
    
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
            }
        }
    }

    private void Start()
    {
        healthSystem = new HealthSystem(20);
        State = ZombieState.Patrol;
    }

    private IEnumerator StatePatrol()
    {
        while (State == ZombieState.Patrol)
        {
            if (Vector2.Distance(transform.position, playerPos.position) < attackDist)
                State = ZombieState.Chase;
                
            yield return null;
        }        
    }
    
    private IEnumerator StateChase()
    {
        while (State == ZombieState.Chase)
        {
            if (Vector2.Distance(transform.position, playerPos.position) < attackDist)
                transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
            else
            {
                State = ZombieState.Patrol;
            }

            yield return null;
        } 
    }

    public virtual void TakeDamage(int dmg)
    {
        healthSystem.Damage(dmg);
        if (healthSystem.GetHealth() <= 0) Die();
    }

    public virtual void Attack()
    {
        Player.getPlayerInstance().HealthSystem.Damage(damage);
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
    Chase
}