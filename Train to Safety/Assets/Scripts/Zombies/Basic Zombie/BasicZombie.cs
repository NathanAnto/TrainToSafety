using System;
using UnityEngine;

public class BasicZombie : Zombie
{
    [SerializeField] private int health;

    private void Update()
    {
        health = healthSystem.GetHealth();
    }
}