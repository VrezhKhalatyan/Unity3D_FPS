using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantStrike : MonoBehaviour {

	public int damage = 5;

	void OnTriggerEnter(Collider other) {
		PlayerController player = other.GetComponent<PlayerController>();
		if(player != null){
			player.Hurt(damage);
		}
	}
}
