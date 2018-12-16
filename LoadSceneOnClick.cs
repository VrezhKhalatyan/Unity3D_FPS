using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//UI SCRIPT TO LOAD SCENE ON SCENE SELECTION INDEX
public class LoadSceneOnClick : MonoBehaviour {

	public void LoadByIndex(int sceneIndex){
		SceneManager.LoadScene(sceneIndex);
	}
}
