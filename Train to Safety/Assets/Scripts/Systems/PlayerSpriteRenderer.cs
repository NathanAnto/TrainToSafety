using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerSpriteRenderer
{
    private List<SpriteRenderer> parts;
    private float offset;
    private float verticalMove;
    private bool facingRight;
    private WeaponListener weaponListener;

    public PlayerSpriteRenderer(bool facingRight, List<Transform> parts)
    {
        this.facingRight = facingRight;

        this.parts = new List<SpriteRenderer>();
        offset = -0.1f;

        foreach (var p in parts) {
            var partSprite = p.GetComponent<SpriteRenderer>();
            this.parts.Add(partSprite);
        }
    }

    public float GetOffset() => offset;

    /// <summary>
    /// Function for sorting the player depending
    /// on where he's going or what he's aiming at
    /// </summary>
    public void SortSprites(float v) {
        verticalMove = v;
        if (parts.Count > 0) {
            foreach (var part in parts) {
                var weapon = part.GetComponent<Weapon>();
                // When going up
                if (verticalMove > 0) {
                    offset = 0.1f;
                    part.sortingOrder = weapon != null ? 18 : 19;
                }
                // When going down
                else if (verticalMove < 0) {
                    offset = -0.1f;
                    part.sortingOrder = weapon != null ? 22 : 23;
                }
            }
        } else {
            if (verticalMove > 0) {
                offset = 0.1f;
            } else if (verticalMove < 0) {
                offset = -0.1f;
            }
        }
    }

    public void ResetRenderers(List<Transform> transforms)
    {
        parts = new List<SpriteRenderer>();
        
        foreach (var t in transforms) {
            var partSprite = t.GetComponent<SpriteRenderer>();
            parts.Add(partSprite);
        }
    }
    
    public void FlipPlayer(Transform tr) {
        Vector2 mouseOnScreen = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerPos = tr.localPosition;
		
        if(	mouseOnScreen.x > playerPos.x && !facingRight ||
            mouseOnScreen.x < playerPos.x && facingRight)				
            RotatePlayer(tr);
    }	
	
    private void RotatePlayer(Transform tr) {
        facingRight = !facingRight;
        tr.Rotate(0f, 180f, 0f);
    }
}
