using UnityEngine;

public class FlyingDemon : MonoBehaviour
{
    public float speed = 1;
    public float xLimit = 10;

    // Update is called once per frame
    void FixedUpdate()
    {
		transform.position += Vector3.left * speed * Time.deltaTime;
        if (transform.position.x < -xLimit || transform.position.x > xLimit)
        {
			Flip();
			speed = -speed;
		}
		
	}

	void Flip()
    {
        Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
    }
}
