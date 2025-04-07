using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
	public float destroyDelay = 0.5f; // Delay before the platform is destroyed
	public Animator animator; // Reference to the Animator component
	AudioSource audioSource;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb.linearVelocity.y <= 0)
            {
				audioSource.Play(); // Play rumble sound
				GetComponent<Collider2D>().enabled = false; // Disable the collider to prevent further collisions
				animator.enabled = true; // Enable the animator to play the animation
				Destroy(transform.parent.gameObject, destroyDelay); // Destroy the platform after the delay
				
			}
            
		}
	}

}
