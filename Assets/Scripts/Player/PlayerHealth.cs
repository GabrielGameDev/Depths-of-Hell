using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Damager damager = collision.GetComponent<Damager>();
		if (damager != null)
		{
			LevelManager.instance.LoadLevel(0);
		}
		
	}
}
