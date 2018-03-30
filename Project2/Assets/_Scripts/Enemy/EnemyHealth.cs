using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	//Manager Access
	EnemyManager enemyManager;
	public GameObject effect;

	void Start(){
		enemyManager = GameObject.Find ("Game_Manager").GetComponent<EnemyManager> ();
	}

	void Update(){
		CheckForDeath ();
	}

	public float currentHealth = 100.0f;
	const float MAX_HEALTH = 100.0f;

	//Take some value of damage. (Can also be used to heal as param can be +/-).
	public void TakeDamage(float damage){
		GetComponent<Animator> ().SetBool ("attack", false);
		GetComponent<Animator> ().Play ("Hit");
		currentHealth -= damage;
		CheckForDeath ();
	}

	//Check to see if we're dead.
	void CheckForDeath(){
		if(currentHealth <= 0.0f){
			Die ();
		}
	}

	//Adds health to an enemy
	void Heal(float amount){
		currentHealth += amount;
		if(currentHealth > MAX_HEALTH){
			currentHealth = MAX_HEALTH;
		}
	}

	//What to do when this enemy dies.
	void Die(){
		//enemyManager.enemies.Remove (this.gameObject);	//Remove manager reference to this enemy

		//Maybe play some animation / Sound / Effect?
		//Could leave a billboard on the spot it died?
		//gameObject.GetComponent<Animator> ().SetBool("bool3", true);
		gameObject.GetComponent<Animator> ().Play ("Defend");
		Instantiate (effect, new Vector3(transform.position.x - 1, transform.position.y + 1, transform.position.z), Quaternion.identity);
		Invoke ("Death", .8f);
	}
		
	void Death()
	{
		Destroy (gameObject);

		GameObject manager = GameObject.Find ("Game_Manager");
		manager.GetComponent<ScoreManager>().playerScore += 10;
        manager.GetComponent<ScoreManager>().playerMoney += 10;
		Destroy (gameObject);	//Destroy the gameObject.

	}

}
