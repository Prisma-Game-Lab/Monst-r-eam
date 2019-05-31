using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutscenePlay : MonoBehaviour
{
	public VideoClip FinalCutscene;

	private void Awake()
	{
		StartCoroutine(WaitVideoOver());
	}

	private IEnumerator WaitVideoOver()
	{
		yield return new WaitForSeconds((float)FinalCutscene.length);
		SceneManager.LoadScene("Credits", LoadSceneMode.Single);
	}
}
