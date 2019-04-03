using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
	[HideInInspector]
	public string SelectedLevel;


	public void ToNextScene(string sceneName)		// Used when loading single scene (i.e. next fase)
	{
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
	}

	public void ToPauseMenu()		//Used to load pause menu ONLY
	{
		SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
	}

	public void ExitPauseMenu()		//To unload pause menu ONLY
	{
		SceneManager.LoadScene("Countdown", LoadSceneMode.Additive);
		SceneManager.UnloadSceneAsync("PauseMenu");
	}

	public void ToAdditiveScene(string sceneName)		//To load other additive scenes, except pause (i.e. Settings)
	{
		SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
	}

	public void ExitAdditiveScene(string sceneName)     // To unload other additive scenes, except pause (i.e. Settings)
	{
		SceneManager.UnloadSceneAsync(sceneName);
	}

	public void ToGame(string sceneName)		// Used to initiate the game with the countdown scene together
	{
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
		SelectedLevel = sceneName;
		SceneManager.LoadScene("Countdown", LoadSceneMode.Additive);
	}

	public void ToPlayAgain()		// Used for the play again button
	{
		SceneManager.LoadScene(SelectedLevel, LoadSceneMode.Single);
	}
}
