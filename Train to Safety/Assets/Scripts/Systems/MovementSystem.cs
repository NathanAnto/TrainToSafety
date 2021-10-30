using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class MovementSystem
{
    private float offset;
    private List<SpriteRenderer> parts;
    private float verticalMove;

    public MovementSystem(List<Transform> parts)
    {
        this.parts = new List<SpriteRenderer>();
        offset = -0.1f;

        foreach (var p in parts)
        {
            var partSprite = p.GetComponent<SpriteRenderer>();
            this.parts.Add(partSprite);
        }
    }

    public void SetVertical(float v)
    {
        verticalMove = v;
    }

    public float GetOffset() => offset;

    public void HandleMovement()
    {
        if (parts.Count > 0)
        {

            foreach (var part in parts)
            {
                // When going up
                var weapon = part.GetComponent<Weapon>();
                if (verticalMove > 0)
                {
                    offset = 0.1f;
                    if (weapon != null)
                        part.sortingOrder = 18; // Weapon
                    else
                        part.sortingOrder = 19; // Hands
                }
                // When going down
                else if (verticalMove < 0)
                {
                    offset = -0.1f;
                    if (weapon != null)
                        part.sortingOrder = 22; // Weapon
                    else
                        part.sortingOrder = 23; // Hands
                }
            }
        }
        else
        {
            if (verticalMove > 0) {
                offset = 0.1f;
            } else if (verticalMove < 0) {
                offset = -0.1f;
            }
        }
    }
}
