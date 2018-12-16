using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperHealthPickup : MonoBehaviour {
	/*The SuperHealthPickup is a special power-up pick up item
	which increases the players health by 60; in camparison with 
	the health kits that only increase the health by 20 */
	[SerializeField] private int _heal = 60;
	
	
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