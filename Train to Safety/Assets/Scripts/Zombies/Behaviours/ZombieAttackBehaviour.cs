using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackBehaviour : Zombie, IZombieBehaviour
{
    public IEnumerator DoBehaviour()
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
            }

            ChangeState();
            AnimationHandler();
            
            yield return null;
        }
    }
}
