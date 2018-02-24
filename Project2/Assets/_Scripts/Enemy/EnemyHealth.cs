using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	//Manager Access
	EnemyManager enemyManager;


	void Start(){
		enemyManager = GameObject.Find ("Game_Manager").GetComponent<EnemyManager> ();
	}

	public int currentHealth = 100;
	const int MAX_HEALTH = 100;

	//Take some value of damage. (Can also be used to heal as param can be +/-).
	public void TakeDamage(int damage){
		currentHealth -= damage;
		CheckForDeath ();
	}

	//Check to see if we're dead.
	void CheckForDeath(){
		if(currentHealth <= 0){
			Die ();
		}
	}

	//What to do when this enemy dies.
	void Die(){
		enemyManager.enemies.Remove (this.gameObject);	//Remove manager reference to this enemy

		//Maybe play some animation / Sound / Effect?
		//Could leave a billboard on the spot it died?

		Destroy (gameObject);	//Destroy the gameObject.
	}

}
