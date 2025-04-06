using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	public GameObject deathFx;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Damager damager = collision.GetComponent<Damager>();
		if (damager != null)
		{
			GameObject newDeathFx = Instantiate(deathFx, transform.position, Quaternion.identity);
			newDeathFx.SetActive(true);			
			LevelManager.instance.GameOver(damager.reload);
			
		}
		
	}
}
