using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMove : MonoBehaviour
{
	public float defaultSpeed;

	private Animator animator;
	private Rigidbody2D rb;
	public PlayerSpriteRenderer playerSpriteRenderer;
	private float animOffset;
	private float speed;
	private float h, v;
	
	private const float MOVELIMITER = 0.7f;
	private static readonly int VelocityX = Animator.StringToHash("VelocityX");
	private static readonly int VelocityY = Animator.StringToHash("VelocityY");
	
	// Start is called before the first frame update
	void Start() {
		animator = transform.GetChild(0).GetComponent<Animator>(); // Get Animator on Body
		rb = GetComponent<Rigidbody2D>();
		speed = defaultSpeed;
		
		var playerWeapon = WeaponHandler.instance.GETCurrentWeapon().gameObject;
		playerSpriteRenderer = new PlayerSpriteRenderer(true,
			new List<Transform>() {
				playerWeapon.transform, // Weapon
				playerWeapon.transform.GetChild(1).transform, // Hand 1
				playerWeapon.transform.GetChild(2).transform // Hand 2
			});
    }

	private void Update() {
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");

		HandleMovement();
	}

	private void HandleMovement() {
		// Check for diagonal movement
		if (h != 0 && v != 0) {
			// limit movement speed diagonally, so you move at 70% speed
			h *= MOVELIMITER;
			v *= MOVELIMITER;
		}
		
		playerSpriteRenderer.SortSprites(v);
		playerSpriteRenderer.FlipPlayer(transform);
		HandleAnimations();
		rb.velocity = new Vector2(h * speed, v * speed);
	}

	private void HandleAnimations() {
		animOffset = playerSpriteRenderer.GetOffset();
		animator.SetFloat(VelocityX, h);
		animator.SetFloat(VelocityY, v+animOffset);
	}
}
