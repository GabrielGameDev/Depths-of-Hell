using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
	public bool spawnLevel;
	private void OnTriggerEnter2D(Collider2D collision)
	{

		if (collision.CompareTag("Player"))
		{
			if (spawnLevel)
			{
				PlatformSpawner.instance.NextLevel();
				Destroy(gameObject);
			}
			else
			{
				Debug.Log("Level Triggered");
				Destroy(gameObject);
			}
			
		}
	}
}
