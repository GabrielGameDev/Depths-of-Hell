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
    public PlatformSettings[] platformSettings;
	Vector2 lastPlatformPosition;
    int index;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        for (int i = 0; i < platformSettings[0].numberOfPlatforms; i++)
        {
            SpawnPlatform();
        }
    }

    void SpawnPlatform()
    {
        float yPos = Random.Range(lastPlatformPosition.y + platformSettings[index].height.x, 
            lastPlatformPosition.y + platformSettings[index].height.y);
        Vector2 randomPos = new Vector2(Random.Range(platformSettings[index].spawnPosition.x, platformSettings[index].spawnPosition.y), yPos);
        int randomIndex = Random.Range(0, platformSettings[index].platformPrefab.Length);
        GameObject platform = Instantiate(platformSettings[index].platformPrefab[randomIndex], randomPos, Quaternion.identity);
        lastPlatformPosition = platform.transform.position;
        
    }
}
