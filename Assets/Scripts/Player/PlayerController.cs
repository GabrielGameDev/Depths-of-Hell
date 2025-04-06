using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; 
	public float stoppingSpeed = 5f;
    public float jumpForce = 5f; 
    public LayerMask groundLayer;
	public float footOffset = 0.5f;
	public float groundCheckDistance = 0.2f;
	public AudioClip[] jumpSounds;
    public Vector2 screenLimit;
	private Rigidbody2D rb; 
    private bool isGrounded;
	public bool hasDoubleJump;
	private bool hasDash;
	public float dashForce = 10;            	
	public float dashDuration = 0.25f;
	bool isDashing;
	float dashTime;
	bool canDash;
	bool usedDoubleJump;
	AudioSource audioSource;
	float targetVelocityX = 0f;
	public bool jumpPressed;

	[Header("Wall")]
	public bool onWall;	
	public float maxFallSpeed = -1;	
	public LayerMask wallLayer;
	public float horizontalJumpForce = 6;
	public float wallJumpTime = 0.5f;
	public float wallCheckDistance = 0.2f;
	int direction = 1; // 1 for right, -1 for left
	Animator animator;


	public bool HasDoubleJump { get => hasDoubleJump; set => hasDoubleJump = value; }
	public bool HasDash { get => hasDash; set => hasDash = value; }

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
		animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is grounded
        RaycastHit2D rightCheck = Raycast(new Vector2(footOffset, 0), Vector2.down, groundCheckDistance, groundLayer);
        RaycastHit2D leftCheck = Raycast(new Vector2(-footOffset, 0), Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = rightCheck || leftCheck;
        
		if((isGrounded || onWall) && Time.time > dashTime)
			canDash = true;
    
		float moveInput = Input.GetAxis("Horizontal");

		// Deadzone para joystick
		if (Mathf.Abs(moveInput) < 0.1f)
			moveInput = 0f;

		targetVelocityX = moveInput * speed;

		Vector3 pos = transform.position;
		pos.x = Mathf.Clamp(pos.x, screenLimit.x, screenLimit.y);		
		transform.position = pos;

		//check wall
		bool onWallCheck = Raycast(Vector2.zero, Vector2.right * direction, wallCheckDistance, wallLayer);		

		if(onWallCheck)
		{
			onWall = true;			
		}
		
		if (onWall)
		{
			
			if (rb.linearVelocity.y < maxFallSpeed)
			{
				rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxFallSpeed);
			}
		}
		

		if (onWall)
			return;
		if (moveInput > 0 && direction < 0)
		{
			Flip();
		}
		else if (moveInput < 0 && direction > 0)
		{
			Flip();
		}

		

	}

	public void Dash(InputAction.CallbackContext ctx)
	{
		if(ctx.performed)
		{
			if (canDash && hasDash)
			{
				isDashing = true;
				dashTime = Time.time + dashDuration;
				canDash = false;
				animator.SetTrigger("dash");
				audioSource.PlayOneShot(jumpSounds[Random.Range(0, jumpSounds.Length)]);
				rb.gravityScale = 0;
				transform.position += Vector3.up * 0.25f;
				rb.linearVelocity = Vector2.zero;          //zera a velocidade do jogador antes de executar o dash
				rb.AddForce(Vector2.right * direction * dashForce, ForceMode2D.Impulse); //adiciona força do dash
				Invoke("ReleaseDash", dashDuration);
			}
		}
	}

	void ReleaseDash()
	{
		isDashing = false;
		rb.gravityScale = 1;
	}

	public void Jump(InputAction.CallbackContext ctx)
	{
		if(ctx.performed)
		{
			if (isGrounded)
			{				
				animator.SetTrigger("jump");
				audioSource.PlayOneShot(jumpSounds[Random.Range(0, jumpSounds.Length)]);			
				rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
				usedDoubleJump = false;
			}
			else if (hasDoubleJump && !usedDoubleJump)
			{
				usedDoubleJump = true;
				rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
				animator.SetTrigger("jump");
				audioSource.PlayOneShot(jumpSounds[Random.Range(0, jumpSounds.Length)]);
				rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
			}
			else if(onWall && !isGrounded)
			{
				animator.SetTrigger("jump");
				audioSource.PlayOneShot(jumpSounds[Random.Range(0, jumpSounds.Length)]);
				rb.AddForce(new Vector2(horizontalJumpForce * -direction, jumpForce), ForceMode2D.Impulse);
				Invoke("ResetOnWall", wallJumpTime);
			}
			
		}
		else if(ctx.canceled)
		{
			rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
		}
	}

	void Flip()
	{
		direction *= -1;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private void FixedUpdate()
	{
		if (onWall || isDashing)
			return;

		float newVelocityX = Mathf.Lerp(rb.linearVelocity.x, targetVelocityX, stoppingSpeed * Time.fixedDeltaTime);
		rb.linearVelocity = new Vector2(newVelocityX, rb.linearVelocity.y);		
		animator.SetFloat("speed", Mathf.Abs(newVelocityX));
	}

	void ResetOnWall()
	{
		onWall = false;
	}

	RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask mask)
	{		
		Vector2 pos = transform.position;
		
		RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, mask);		

		Color color = hit ? Color.red : Color.green;
		
		Debug.DrawRay(pos + offset, rayDirection * length, color);

		return hit;
	}

	
}
