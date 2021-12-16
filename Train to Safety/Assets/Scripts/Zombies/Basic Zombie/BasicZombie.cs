using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class BasicZombie : Zombie
{
    [SerializeField] private float bzSpeed = 2;
    [SerializeField] private float bzAttackDist = 1.5f;
    [SerializeField] private int bzAttackRate = 3;
    [SerializeField] private int bzRange = 10;

    private void Start()
    {
        speed = bzSpeed;
        attackDist = bzAttackDist;
        attackRate = bzAttackRate;
        range = bzRange;
    }

    private void Update()
    {
        Debug.Log(State);
        playerPos = GameObject.Find("Dwight").transform;
        if (speed < bzSpeed) {
            speed += .1f;
        }
        else if(speed >= bzSpeed) speed = bzSpeed;
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