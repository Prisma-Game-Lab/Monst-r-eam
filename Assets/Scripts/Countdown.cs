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

	public Camera cam;

	private IEnumerator CountdownTimer()
	{
		string str = SaveSystem.GetInstance().currLevelString;
		AsyncOperation async = SceneManager.LoadSceneAsync(str, LoadSceneMode.Additive);
		//async.allowSceneActivation = false;
		Debug.Log("Game Scene is now loading");
		Time.timeScale = 0;
		float t = 0.0f;
		while(t < CountdownTime)
		{
			yield return new WaitForSecondsRealtime(0.3f);
			t += 0.3f;
			if(async.isDone)
			{
				cam.enabled = false;
			}
		}
		Time.timeScale = 1;
		Debug.Log("Countdown is over?");
		yield return new WaitUntil(() => async.isDone);
		SceneManager.UnloadSceneAsync("Countdown");	//Unload countdown scene
		yield break;
		
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

		Debug.Log(_tempTime.ToString());
		CountdownText.text = _tempTime.ToString();
		//Debug.Log(_tempTime);
	}

}