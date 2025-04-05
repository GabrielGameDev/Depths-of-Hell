using UnityEngine;

[System.Serializable]
public class PlatformSettings
{
	public GameObject[] platformPrefab;
	public Vector2 spawnPosition;
	public Vector2 height;
	public int numberOfPlatforms;
	
}

public class PlatformSpawner : MonoBehaviour
{
    public static PlatformSpawner instance;
	public GameObject levelTrigger, nextlevelNotification;
	public Mover lava;
	public float lavaIncreaseSpeed;
    public PlatformSettings[] platformSettings;
	Vector2 lastPlatformPosition;
    int index;

	private void Awake()
	{
		instance = this;
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        NextLevel();
    }

    void SpawnPlatform(int i)
    {
        float yPos = Random.Range(lastPlatformPosition.y + platformSettings[index].height.x, 
            lastPlatformPosition.y + platformSettings[index].height.y);
        Vector2 randomPos = new Vector2(Random.Range(platformSettings[index].spawnPosition.x, platformSettings[index].spawnPosition.y), yPos);
        int randomIndex = Random.Range(0, platformSettings[index].platformPrefab.Length);
        GameObject platform = Instantiate(platformSettings[index].platformPrefab[randomIndex], randomPos, Quaternion.identity);
        lastPlatformPosition = platform.transform.position;
        if(i == platformSettings[index].numberOfPlatforms - 1) 
		{ 
			Instantiate(nextlevelNotification, new Vector2(platform.transform.position.x, platform.transform.position.y), Quaternion.identity);
		}
		if (i == platformSettings[index].numberOfPlatforms / 2)
		{
			Instantiate(levelTrigger, new Vector2(platform.transform.position.x, platform.transform.position.y), Quaternion.identity);
		}
    }

    public void NextLevel()
    {
		for (int i = 0; i < platformSettings[index].numberOfPlatforms; i++)
		{
			SpawnPlatform(i);
		}
		index++;
		lava.moveSpeed *= lavaIncreaseSpeed;
	}
}
