using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
	[Range(0, 100)] public float CountdownTime = 2;

	private IEnumerator CountdownTimer()
	{
		yield return new WaitForSeconds(CountdownTime);
		SceneManager.UnloadSceneAsync("Countdown");	//Unload countdown scene
	}

	private void Start()
	{
		StartCoroutine("CountdownTimer");	// Start scene countdown
	}
}
