using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutscenePlay : MonoBehaviour
{
	public VideoClip FinalCutscene;
	[SerializeField]
	private string SceneDestination;

	private void Awake()
	{
		StartCoroutine(WaitVideoOver());
	}

	private IEnumerator WaitVideoOver()
	{
		yield return new WaitForSeconds((float)FinalCutscene.length);
		SceneManager.LoadScene(SceneDestination, LoadSceneMode.Single);
	}
}
