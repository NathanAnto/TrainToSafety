using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BasicZombieBehaviour : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private float attackDist;
    [SerializeField] private Transform target;
    
    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (Vector2.Distance(transform.position, target.position) < attackDist)
        {
            // Attack();
        }
        else
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                target.position,
                speed * Time.deltaTime);
        }
    }

    private void Die() {
        Destroy(gameObject);
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
            Die();
    }
}
