using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	public float speed;
	private float defaultSpeed;
	public int health;
	private float moveLimiter = 0.7f;

	private Rigidbody2D rb;
	private Animator animator;

	private Vector2 movement;
	private bool facingRight;
	private bool isAiming;
	private bool isMoving;
	private float horizontal;
	private float vertical;

	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		facingRight = true;
		defaultSpeed = speed;
	}

	void Update()
	{
		// input
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");
		handleAiming();
		flipPlayer(horizontal);
	}


    // Fixed Update is called 50 times per second
    void FixedUpdate()
    {
		// movement
		handleMovement(horizontal, vertical);
	}

	private void handleMovement(float horizontal, float vertical)
	{
		// Set animations
		if (horizontal == 0 && vertical == 0)  {		
			isMoving = false;
			animator.SetBool("isMoving", isMoving);
		}
		else  {
			isMoving = true;
			animator.SetBool("isMoving", isMoving);
		}
		// slow down animations if aiming and moving
		if(isAiming && isMoving) {
			animator.SetFloat("speedMultiplier", 0.5f);
			speed = 1f;
		}
		else {
			animator.SetFloat("speedMultiplier", 1f);
			speed = defaultSpeed;
		}

		if (horizontal != 0 && vertical != 0) // Check for diagonal movement
		{
			// limit movement speed diagonally, so you move at 70% speed
			horizontal *= moveLimiter;
			vertical *= moveLimiter;
		}

		rb.velocity = new Vector2(horizontal * speed, vertical * speed);
	}

	private void handleAiming() {
		if(Input.GetMouseButtonDown(1)) { // right mouse click
			isAiming = true;
			animator.SetBool("isAiming", isAiming);
		}
		else if(Input.GetMouseButtonUp(1)) {
			isAiming = false;
			animator.SetBool("isAiming", isAiming);
		}
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

		if(isAiming) {						
			// Transforming mouse pos to resemble player pos
    		Vector2 mouseOnScreen = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 playerPos = transform.localPosition;

        	float angle = AngleBetweenTwoPoints(playerPos, mouseOnScreen);

			Vector3 scale = transform.localScale;

			if(mouseOnScreen.x > playerPos.x && !facingRight || mouseOnScreen.x < playerPos.x && facingRight) {
				facingRight = !facingRight;
				scale.x *= -1;
				transform.localScale = scale;
				// GetComponentsInChildren<Transform>()[0].transform.rotation = Quaternion.Euler (new Vector3(0f,0f,angle));
			}
		}
	}
	private float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
