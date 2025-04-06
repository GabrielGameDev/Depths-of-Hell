using TMPro;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlatformSettings
{
	public GameObject[] platformPrefab;
	public GameObject[] enemyPrefab;
	public float spawnEnemyChance;
	public Vector2 spawnPosition;
	public Vector2 height;
	public int numberOfPlatforms;
	public AudioClip levelSound;
	public string announcementText;
	public UnityEvent OnStartLevel;
	
}

public class PlatformSpawner : MonoBehaviour
{
    public static PlatformSpawner instance;
	public GameObject levelTrigger, nextlevelNotification;
	public Mover lava;
	public Renderer backgroundScroller;
	public float lavaIncreaseSpeed;
	public TMP_Text levelText;
	public TMP_Text announcementText;
	public GameObject announcementPanel;
    public PlatformSettings[] platformSettings;
	Vector2 lastPlatformPosition;
    int index;
	AudioSource audioSource;

	private void Awake()
	{
		instance = this;
		audioSource = GetComponent<AudioSource>();
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {		
		NextLevel();
		UpdateLevelText();
	}

    void SpawnPlatform(int i)
    {
        float yPos = Random.Range(lastPlatformPosition.y + platformSettings[index].height.x, 
            lastPlatformPosition.y + platformSettings[index].height.y);
        Vector2 randomPos = new Vector2(Random.Range(platformSettings[index].spawnPosition.x, platformSettings[index].spawnPosition.y), yPos);
        int randomIndex = Random.Range(0, platformSettings[index].platformPrefab.Length);
        GameObject platform = Instantiate(platformSettings[index].platformPrefab[randomIndex], randomPos, Quaternion.identity);
        lastPlatformPosition = platform.transform.position;
		if(Random.value < platformSettings[index].spawnEnemyChance)
		{
			Instantiate(platformSettings[index].enemyPrefab[Random.Range(0, platformSettings[index].enemyPrefab.Length)],
				platform.transform.position + Vector3.up, Quaternion.identity);
		}
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
		Vector2 scrollDirection = backgroundScroller.material.GetVector("_ScrollDirection");
		scrollDirection.y *= lavaIncreaseSpeed;
		backgroundScroller.material.SetVector("_ScrollDirection", scrollDirection);
	}

	public async void UpdateLevelText()
	{
		platformSettings[index - 1].OnStartLevel.Invoke();		
		levelText.text = "DEPTH " + (platformSettings.Length - index);
		announcementPanel.SetActive(true);
		announcementText.text = platformSettings[index - 1].announcementText;
		await Awaitable.WaitForSecondsAsync(0.5f);
		audioSource.PlayOneShot(platformSettings[index - 1].levelSound);
		await Awaitable.WaitForSecondsAsync(3.5f);
		announcementPanel.SetActive(false);
	}



	
}
