using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; 
    public float jumpForce = 5f; 
    public LayerMask groundLayer;
	public float footOffset = 0.5f;
	public float groundCheckDistance = 0.2f;
	private Rigidbody2D rb; 
    private bool isGrounded;
    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is grounded
        RaycastHit2D rightCheck = Raycast(new Vector2(footOffset, 0), Vector2.down, groundCheckDistance, groundLayer);
        RaycastHit2D leftCheck = Raycast(new Vector2(-footOffset, 0), Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = rightCheck || leftCheck;
        
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
			rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
		}
        if(Input.GetButtonUp("Jump"))
        {
			rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
		}
        
    }

	private void FixedUpdate()
	{
		float moveInput = Input.GetAxis("Horizontal");
		rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
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
