using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	public float speed = 5f;
	public int health = 3;
	private float moveLimiter = 0.7f;

	private Rigidbody2D rb;
	private Animator[] animators;

	private Vector2 movement;
	private bool facingRight;
	private float horizontal;
	private float vertical;

	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		animators = new Animator[3];
		for(int i = 0; i < animators.Length; i++) {
			animators[i] = GetComponentsInChildren<Animator>()[i];
		}
		facingRight = true;
	}

	void Update()
	{
		// input
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");
	}


    // Fixed Update is called 50 times per second
    void FixedUpdate()
    {
		// movement
		handleMovement(horizontal, vertical);
		flipPlayer(horizontal);
	}

	private void handleMovement(float horizontal, float vertical)
	{
		// Set animations
		if (horizontal == 0 && vertical == 0)  {			
			foreach(Animator animator in animators) {
				animator.SetBool("isMoving", false);
			}
		}
		else  {
			foreach(Animator animator in animators) {
				animator.SetBool("isMoving", true);
			}		
		}

		if (horizontal != 0 && vertical != 0) // Check for diagonal movement
		{
			// limit movement speed diagonally, so you move at 70% speed
			horizontal *= moveLimiter;
			vertical *= moveLimiter;
		}

		rb.velocity = new Vector2(horizontal * speed, vertical * speed);
	}

	private void flipPlayer(float horizontal)
	{
		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
		{
			facingRight = !facingRight;

			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		}
	}
}
