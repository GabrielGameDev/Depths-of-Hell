using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
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
	public static bool hardcoreMode = false;
	public Volume finishGameVolume;
	public bool isGameOver = false;
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
		
		if (reload)
		{	
			cineCam.enabled = false;
			lava.enabled = true;
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

	public void FInishGame()
	{
		if (isGameOver)
			return;
		isGameOver = true;
		StartCoroutine(BlendingVolume());
	}

	IEnumerator BlendingVolume()
	{
		float value = 0;
		while (value < 1)
		{
			value += Time.deltaTime / 2;
			finishGameVolume.weight = value;
			music.volume = Mathf.Lerp(music.volume, 0, value);
			yield return null;
		}

		yield return new WaitForSeconds(1f);
		if(hardcoreMode)
		{
			SceneManager.LoadScene(2);
		}
		else
		{
			SceneManager.LoadScene(3);
		}

	}
}
