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
				PlatformSpawner.instance.UpdateLevelText();
				Destroy(gameObject);
			}
			
		}
	}
}
