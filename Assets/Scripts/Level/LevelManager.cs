using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
	public AudioSource music;
	public Mover cineCam;
	public Mover lava;
	public PlayerController playerController;
	public TMP_Text deathCountText;
	int deathCount = 0;

	private void Awake()
	{
		instance = this;
	}

	public void LoadLevel(int buildIndex)
	{
		SceneManager.LoadScene(buildIndex);
	}

	public async void GameOver(bool reload)
	{
		deathCount++;
		deathCountText.text = "X " + deathCount;
		//cineCam.enabled = false;
		//lava.enabled = true;
		if (reload)
		{
			await Awaitable.WaitForSecondsAsync(3.25f);
			LoadLevel(0);
		}
		
	}

	public async void PlayMusic(float delay)
	{
		music.PlayDelayed(delay);
		await Awaitable.WaitForSecondsAsync(delay / 2);
		playerController.enabled = true;
		await Awaitable.WaitForSecondsAsync(delay/2);
		cineCam.enabled = true;
		
	}
}
