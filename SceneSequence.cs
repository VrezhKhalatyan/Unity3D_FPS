using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSequence : MonoBehaviour {
	
	public GameObject Cam1;
	public GameObject Cam2;
	public GameObject Cam3;

	// Use this for initialization
	void Start () {
		StartCoroutine(TheSequence());
	}
	
	IEnumerator TheSequence(){
		
		yield return new WaitForSeconds(4);
		Cam2.SetActive(true);
		Cam1.SetActive(false);
		yield return new WaitForSeconds(4);
		Cam3.SetActive(true);
		Cam2.SetActive(false);
		yield return new WaitForSeconds(4);
		
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}
}
