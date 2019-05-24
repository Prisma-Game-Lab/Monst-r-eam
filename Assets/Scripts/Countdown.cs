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
	[Range(0, 100)] public float CountdownTime = 3;

	[SerializeField]
	private Text CountdownText;

	private float _textTime;
	private int _tempTime;

	private IEnumerator CountdownTimer()
	{
		Time.timeScale = 0;
		yield return new WaitForSecondsRealtime(CountdownTime);
		Time.timeScale = 1;
		SceneManager.UnloadSceneAsync("Countdown");	//Unload countdown scene
	}

	private void Start()
	{
		_textTime = CountdownTime;
		StartCoroutine("CountdownTimer");	// Start scene countdown
	}

	private void Update()
	{
		_textTime -= Time.fixedDeltaTime;
		_tempTime = Mathf.RoundToInt(_textTime);

		if (_tempTime <= 0) _tempTime = 0;

		CountdownText.text = _tempTime.ToString();
		//Debug.Log(_tempTime);
	}

}