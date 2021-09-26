using System;
using System.Collections;
using UnityEngine;

public class BasicZombie : Zombie
{
    [SerializeField] private int health;
    [SerializeField] private ZombieState state;

    private void Start()
    {
        attackDist = 1.5f;
        attackRate = 3;
        speed = 2;
        range = 10;
    }

    private void Update()
    {
        health = healthSystem.GetHealth();
        state = zombieState;
        playerPos = GameObject.Find("Dwight").transform;
    }
    
    protected override IEnumerator StatePatrol()
    {
        return base.StatePatrol();
    }
    
    protected override IEnumerator StateChase()
    {
        return base.StateChase();
    }

    protected override IEnumerator StateAttack()
    {
        return base.StateAttack();
    }
}