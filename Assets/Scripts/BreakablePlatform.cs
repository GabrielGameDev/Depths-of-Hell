using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
	public float destroyDelay = 0.5f; // Delay before the platform is destroyed
	private void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.CompareTag("Player"))
		{			
			if(collision.transform.position.y > transform.position.y)
			{
				Destroy(gameObject, destroyDelay);
			}
		}
    }
}
