using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Through the box collider the player
is able to pick up a health kit in order to increase it's health */
/*Health kits are box shaped objects with a red cross and also
contains a red particle system */
public class HealthPickup : MonoBehaviour {

	[SerializeField] private int _heal = 30;
	
	
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "mutant" || other.gameObject.tag == "mutant2" || other.gameObject.tag == "byStander"){
			return;
		}
		PlayerController player = other.GetComponent<PlayerController>();
		if(player.health == 100){
			return;
		}
		if(player != null){
			player.HealthP(_heal);
		}
		Destroy(this.gameObject);
	}
}
