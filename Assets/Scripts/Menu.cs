using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject sounds;
    public AudioSource music;
    bool selected;
    public async void NormalMode()
    {
        if (selected) return;
        StartCoroutine(FadinMusic());
        selected = true;
        sounds.SetActive(true);
        LevelManager.hardcoreMode = false;
        await Awaitable.WaitForSecondsAsync(3f);
        SceneManager.LoadScene(1);
    }
    public async void HardcoreMode()
    {
        if (selected) return;
        StartCoroutine(FadinMusic());
        selected = true;
        sounds.SetActive(true);		
		LevelManager.hardcoreMode = true;
		await Awaitable.WaitForSecondsAsync(3f);
		SceneManager.LoadScene(1);
	}

    IEnumerator FadinMusic()
    {
		float fadeDuration = 1f;
		float startVolume = music.volume;
		for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
			music.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
			yield return null;
		}
		music.volume = 0;
	}

}
