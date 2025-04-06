using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
	public AudioSource music;
	public Mover cineCam;
	public PlayerController playerController;

	private void Awake()
	{
		instance = this;
	}

	public void LoadLevel(int buildIndex)
	{
		SceneManager.LoadScene(buildIndex);
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
