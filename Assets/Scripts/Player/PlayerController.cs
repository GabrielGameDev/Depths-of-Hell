using Unity.VisualScripting;
using UnityEngine;

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
	private bool canDoubleJump;
	bool usedDoubleJump;
	AudioSource audioSource;
	float targetVelocityX = 0f;
	public bool jumpPressed;

	[Header("Wall")]
	public bool onWall;
	public Vector3 wallOffset;
	public float wallRadius;
	public float maxFallSpeed = -1;
	public float wallJumpDuration = 0.25f;
	public bool jumpFromWall;
	public float jumpFinish;
	public LayerMask wallLayer;
	bool canMove = true;
	public float horizontalJumpForce = 6;
	public int direction = 1;

	public bool CanDoubleJump { get => canDoubleJump; set => canDoubleJump = value; }

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is grounded
        RaycastHit2D rightCheck = Raycast(new Vector2(footOffset, 0), Vector2.down, groundCheckDistance, groundLayer);
        RaycastHit2D leftCheck = Raycast(new Vector2(-footOffset, 0), Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = rightCheck || leftCheck;
        
        if(Input.GetButtonDown("Jump") && (isGrounded))
        {
			audioSource.PlayOneShot(jumpSounds[Random.Range(0, jumpSounds.Length)]);			
			rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            usedDoubleJump = false;
		}
        else if(Input.GetButtonDown("Jump") && (canDoubleJump && !usedDoubleJump))
        {
			audioSource.PlayOneShot(jumpSounds[Random.Range(0, jumpSounds.Length)]);
			usedDoubleJump = true;            
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);			
		}
		else if (jumpPressed && onWall && !isGrounded)
		{
			canMove = false;
			jumpFinish = Time.time + wallJumpDuration;
			jumpPressed = false;
			jumpFromWall = true;
			//Flip();

			rb.linearVelocity = Vector2.zero;

			rb.AddForce(new Vector2(horizontalJumpForce * direction, jumpForce), ForceMode2D.Impulse);
		}
		if (Input.GetButtonUp("Jump"))
        {
			rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
		}
		float moveInput = Input.GetAxis("Horizontal");

		// Deadzone para joystick
		if (Mathf.Abs(moveInput) < 0.1f)
			moveInput = 0f;

		targetVelocityX = moveInput * speed;

		Vector3 pos = transform.position;
		pos.x = Mathf.Clamp(pos.x, screenLimit.x, screenLimit.y);		
		transform.position = pos;

		//check wall
		bool rightWall = Physics2D.OverlapCircle(transform.position + new Vector3(wallOffset.x, 0), wallRadius, wallLayer);
		bool leftWall = Physics2D.OverlapCircle(transform.position + new Vector3(-wallOffset.x, 0), wallRadius, wallLayer);
		if (rightWall || leftWall)
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
	}

	private void FixedUpdate()
	{
		float newVelocityX = Mathf.Lerp(rb.linearVelocity.x, targetVelocityX, stoppingSpeed * Time.fixedDeltaTime);
		rb.linearVelocity = new Vector2(newVelocityX, rb.linearVelocity.y);

		
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
