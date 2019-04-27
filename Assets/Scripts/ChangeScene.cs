using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * 
 * Script for loading scenes.
 * 
 * Author: Hugo Tonette
 * 
 */

public class ChangeScene : MonoBehaviour
{
	[HideInInspector]
	public static string SelectedLevel;

	public GameObject PauseMenuGO;
	public GameObject GameUIGO;
	public GameObject VictoryGO;
	public GameObject LoseGO;

	public void ToNextScene(string sceneName)       // Used when loading single scene (i.e. next fase)
	{
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
	}

	public void ToPauseMenu()       //Used to load pause menu ONLY
	{
		if (PauseMenuGO != null && GameUIGO != null)
		{
			PauseMenuGO.SetActive(true);
			GameUIGO.SetActive(false);
			Time.timeScale = 0;
		}
		else
			Debug.Log("PauseMenu and/or GameUI not present");
		
	}

	public void ExitPauseMenu()     //To unload pause menu ONLY
	{
		if (PauseMenuGO != null && GameUIGO != null)
		{
			PauseMenuGO.SetActive(false);
			GameUIGO.SetActive(true);
			Time.timeScale = 1f;
		}
		else
			Debug.Log("PauseMenu and/or GameUI not present");
	}

	public void ToAdditiveScene(string sceneName)       //To load other additive scenes, except pause (i.e. Settings)
	{
		SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
	}

	public void ExitAdditiveScene(string sceneName)     // To unload other additive scenes, except pause (i.e. Settings)
	{
		SceneManager.UnloadSceneAsync(sceneName);
	}

	public void ToGame(string sceneName)        // Used to initiate the game with the countdown scene together
	{
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
		SelectedLevel = sceneName;
		Debug.Log("Nome: " + SelectedLevel);
		SceneManager.LoadScene("Countdown", LoadSceneMode.Additive);
	}

	public void ToPlayAgain()       // Used for the play again button
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
	}

	public void VictoryScreen()
	{
		if (VictoryGO != null)
		{
			VictoryGO.SetActive(true);
		}
		else
			Debug.Log("VictoryGO not present");
	}

	public void LoseScreen()
	{
		if (LoseGO != null)
		{
			LoseGO.SetActive(true);
		}
		else
			Debug.Log("VictoryGO not present");
	}

	public void ToNextLevel()
	{
		// WIP
	}

	public void UnregisterPlayerInput()
	{
		PlayerInput.Unregister();
	}

	public void ToReloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
