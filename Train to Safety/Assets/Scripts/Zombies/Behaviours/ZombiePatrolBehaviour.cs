using  System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePatrolBehaviour : Zombie, IZombieBehaviour
{
    public IEnumerator DoBehaviour()
    {
        while (State == ZombieState.Patrol)
        {
            velocity = Vector2.zero;

            ChangeState();
            AnimationHandler();
            
            yield return null;
        }
    }
}
