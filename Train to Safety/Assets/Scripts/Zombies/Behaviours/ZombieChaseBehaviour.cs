using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieChaseBehaviour : Zombie, IZombieBehaviour
{
    public IEnumerator DoBehaviour()
    {
        while (State == ZombieState.Chase)
        {
            playerPos = GameObject.FindGameObjectWithTag("Player").transform;
            var tr = transform;
            var pPos = playerPos.position; // Player Position 
            var zPos = tr.position; // Zombie Position 
            var dir = pPos - zPos;
            
            dir.Normalize();
            
            rb.MovePosition(zPos + (dir * (speed * Time.deltaTime)));

            velocity.x = facingRight ? speed : -speed;
            velocity.y = pPos.y > zPos.y ? speed : -speed;
            
            ChangeState();
            AnimationHandler();

            yield return null;
        }
    }
}
