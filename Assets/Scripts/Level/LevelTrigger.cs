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
				FindFirstObjectByType<PlatformSpawner>().NextLevel();
				Destroy(gameObject);
			}
			else
			{
				FindFirstObjectByType<PlatformSpawner>().UpdateLevelText();
				Destroy(gameObject);
			}
			
		}
	}
}
