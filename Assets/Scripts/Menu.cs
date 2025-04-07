using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void NormalMode()
    {
        LevelManager.hardcoreMode = false;
        SceneManager.LoadScene(1);
    }
    public void HardcoreMode()
    {
		LevelManager.hardcoreMode = true;
		SceneManager.LoadScene(1);
	}
}
