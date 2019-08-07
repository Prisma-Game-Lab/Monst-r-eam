using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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

	public int MaxLevelPerBlock = 6;

	[Space]
	public GameObject PauseMenuGO;
	public GameObject GameUIGO;
	public GameObject VictoryGO;
	public GameObject LoseGO;

	public void ToLoadScene(string sceneName)       // Used when loading single scene (i.e. next fase)
	{
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        Vitoria.WinCount = 0; 
        Morte.DeathCount = 0; 
    }

	public void ToPauseMenu()       //Used to load pause menu ONLY
	{
		if (PauseMenuGO != null && GameUIGO != null && SceneManager.GetActiveScene().name != "Countdown")
		{
			PauseMenuGO.SetActive(true);
			GameUIGO.SetActive(false);
			Time.timeScale = 0;
		}
		else
			Debug.Log("PauseMenu and/or GameUI not present and/or Countdown is on");

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
		SelectedLevel = sceneName;
		Debug.Log("Nome: " + SelectedLevel);
		

		SaveSystem.GetInstance().currLevelString = sceneName;
		SceneManager.LoadScene("Countdown", LoadSceneMode.Single);
		//StartCoroutine(LoadYourAsyncScene(sceneName));		
		
		//SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
		//SceneManager.LoadScene("Countdown", LoadSceneMode.Additive);
		
		
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
		//gente, que perigo essa função, mas a vida é pra viver perigosamente

		string sTemp = SceneManager.GetActiveScene().name;
		string sChar = sTemp.Substring(sTemp.Length - 1);

		string sRest = sTemp.Remove(sTemp.Length - 1);
		int tempLvl = 0;
		Int32.TryParse(sChar, out tempLvl);

		tempLvl++;
		if (tempLvl > MaxLevelPerBlock)
		{
			SceneManager.LoadScene("LevelSelect");
		}
		else
		{
			SaveSystem.GetInstance().currLevelNumber++;

			//esse load mudou quando eu mudei o esquema do countdown
			SaveSystem.GetInstance().currLevelString = sRest + tempLvl.ToString(); //primeiro seto a string pro countdown loadar
			SceneManager.LoadScene("Countdown", LoadSceneMode.Single); //depois loado só cena de countdown e ela faz o resto
			
			//SceneManager.LoadScene(sRest + tempLvl.ToString());
			
		}
		Morte.DeathCount = 0;
		Vitoria.WinCount = 0;
	}

	//essa função ainda é usada em algum lugar? Ela me parece redundante com a ToLoadScene, acima. Tem algum caso em que faça diferença? Ass: Krauss
	public void LoadSceneNormal(string name)
	{
		SceneManager.LoadScene(name, LoadSceneMode.Single);
	}

	public void UnregisterPlayerInput()
	{
		PlayerInput.Unregister();
	}

	public void ToReloadScene()
	{
		Morte.DeathCount = 0;
		Vitoria.WinCount = 0;
		//SaveSystem.GetInstance().CountdownDontReloadScene = true;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
		
	}

	IEnumerator LoadYourAsyncScene(string SceneName)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
