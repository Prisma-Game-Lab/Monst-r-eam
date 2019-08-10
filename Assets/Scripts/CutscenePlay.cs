using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class CutscenePlay : MonoBehaviour
{
	public VideoClip FinalCutscene;
	[SerializeField]
	private string SceneDestination;
	private void Awake()
	{
		StartCoroutine(WaitVideoOver());
	}

	private void Update()
	{
		if(SplashScreen.isFinished) {
			if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space)) {
				GoToNextScene(SceneDestination);
			}
		}
	}

	private IEnumerator WaitVideoOver()
	{
		yield return new WaitForSeconds((float)FinalCutscene.length);
		GoToNextScene(SceneDestination);
	}
	
	private void GoToNextScene (string s)
	{
		SceneManager.LoadScene(s, LoadSceneMode.Single);
	}
}
