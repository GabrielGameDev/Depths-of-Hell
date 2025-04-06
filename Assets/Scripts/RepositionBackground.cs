using UnityEngine;

public class RepositionBackground : MonoBehaviour
{

    Collider2D backgroundCollider; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backgroundCollider = GetComponent<Collider2D>();   
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.CompareTag("Camera"))
        {
			float yPos = transform.position.y + (backgroundCollider.bounds.size.y * 2);
			Vector3 newPosition = new Vector3(transform.position.x, yPos , transform.position.z);
			transform.position = newPosition; // Move the background up by its height
		}
	}
}
