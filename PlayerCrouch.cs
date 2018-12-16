using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour {
	/*If the C button is pressed the player's height is decreased
	creating a crouching effect and is re-stated when the 
	C button is un-pressed */
	CharacterController  characterC;

	void  Start() {
		characterC = gameObject.GetComponent<CharacterController>();
	}

	void Update() {
		if(Input.GetKey(KeyCode.C)){
			characterC.height = 1.0f;
		}
		else{
			characterC.height = 1.8f;
		}
	}
}
