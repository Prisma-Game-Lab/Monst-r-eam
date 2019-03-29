using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
	[HideInInspector]
	public string SelectedLevel;


	public void ToNextScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
	}

	public void ToPauseMenu()
	{
		SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
	}

	public void ExitPauseMenu()
	{
		SceneManager.LoadScene("Countdown", LoadSceneMode.Additive);
		SceneManager.UnloadSceneAsync("PauseMenu");
	}

	public void ToGame(string sceneName)
	{
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
		SelectedLevel = sceneName;
		SceneManager.LoadScene("Countdown", LoadSceneMode.Additive);
	}

	public void ToPlayAgain()
	{
		SceneManager.LoadScene(SelectedLevel, LoadSceneMode.Single);
	}
}
