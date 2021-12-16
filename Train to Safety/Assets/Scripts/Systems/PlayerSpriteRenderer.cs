using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerSpriteRenderer
{
    private Transform weaponTransform;
    private float offset;
    private float verticalMove;
    private bool facingRight;
    private WeaponListener weaponListener;

    public PlayerSpriteRenderer(bool facingRight, Transform wt = null) {
        this.facingRight = facingRight;
        ResetRenderers(wt);
    }

    public float GetOffset() => offset;

    /// <summary>
    /// Function for sorting the player depending
    /// on where he's going or what he's aiming at
    /// </summary>
    public void SortSprites(float v) {
        verticalMove = v;
        if(verticalMove > 0)
            weaponTransform.GetComponent<SpriteRenderer>().sortingOrder = 18; // Set weapon behind player body
        else if(verticalMove < 0)
            weaponTransform.GetComponent<SpriteRenderer>().sortingOrder = 22; // Set weapon in front of player body

        if (weaponTransform == null) return;
        // For hands to appear behind player when
        // going up and in front when going down
        foreach (Transform child in weaponTransform) {
            var sprite = child.GetComponent<SpriteRenderer>();
            if (sprite != null) {
                // When going up
                if (verticalMove > 0) {
                    offset = 0.1f;
                    sprite.sortingOrder = 19; // Set hand behind player body
                }
                // When going down
                else if (verticalMove < 0) {
                    offset = -0.1f;
                    sprite.sortingOrder = 23; // Set hand in front of player body
                }
            }
        }
    }

    public void ResetRenderers(Transform wt) {
        weaponTransform = wt;
        SortSprites(verticalMove+offset);
    }
    
    public void FlipPlayer(Transform tr) {
        var mouseOnScreen = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var pos = tr.localPosition;
		
        if(	mouseOnScreen.x > pos.x && !facingRight || 
            mouseOnScreen.x < pos.x && facingRight)				
            RotatePlayer(tr);
    }	
	
    private void RotatePlayer(Transform tr) {
        facingRight = !facingRight;
        tr.Rotate(0f, 180f, 0f);
    }
}
