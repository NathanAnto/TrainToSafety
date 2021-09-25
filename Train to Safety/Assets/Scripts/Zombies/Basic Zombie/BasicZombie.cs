using System;
using UnityEngine;

public class BasicZombie : Zombie
{
    [SerializeField] private int health;
    [SerializeField] private ZombieState state;
    private float offset = -0.1f;
    private string velocityX = "VelocityX";
    private string velocityY = "VelocityY";

    private void Update()
    {
        health = healthSystem.GetHealth();
        state = zombieState;
        playerPos = GameObject.Find("Dwight").transform;

        movementSystem.SetVertical(rb.velocity.y + offset);
        movementSystem.HandleMovement();
        offset = movementSystem.GetOffset();

        animator.SetFloat(velocityY, rb.velocity.y + offset);
        animator.SetFloat(velocityX, rb.velocity.x);

        Debug.Log(rb.velocity);
    }
}