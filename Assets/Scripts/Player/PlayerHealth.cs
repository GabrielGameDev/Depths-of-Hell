using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	public GameObject deathFx;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Damager damager = collision.GetComponent<Damager>();
		if (damager != null)
		{
			deathFx.transform.position = transform.position;
			deathFx.SetActive(true);			
			LevelManager.instance.GameOver();
			gameObject.SetActive(false);
		}
		
	}
}
