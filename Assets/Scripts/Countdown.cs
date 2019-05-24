using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * 
 * Script for the countdown before the game play
 * 
 * Author: Hugo Tonette
 * 
 */

public class Countdown : MonoBehaviour
{
	[Range(0, 100)] public float CountdownTime = 2;

	[SerializeField]
	private Text CountdownText;

	private IEnumerator CountdownTimer()
	{
		Time.timeScale = 0;
		yield return new WaitForSecondsRealtime(CountdownTime);
		Time.timeScale = 1;
		SceneManager.UnloadSceneAsync("Countdown");	//Unload countdown scene
	}

	private void Start()
	{
		StartCoroutine("CountdownTimer");	// Start scene countdown
	}

	private void FixedUpdate()
	{
		CountdownTime -= Time.unscaledDeltaTime;
		CountdownText.text = Mathf.Round(CountdownTime).ToString();
	}

}
