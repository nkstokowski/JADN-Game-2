using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour {

	public GameObject trapManagerObject;
	TrapManager trapManager;
	float damage;
	float radius;
	float degradeAmount;
	float health;
	TrapType type;

	void Start(){

		//Load our trap stats

		trapManager = trapManagerObject.GetComponent<TrapManager> ();
		for(int i=0;i<trapManager.traps.Length;i++){
			if(trapManager.traps[i].type == type){
				//Load stats
				damage = trapManager.traps[i].damage;
				radius = trapManager.traps[i].radius;
				degradeAmount = trapManager.traps [i].degradeAmount;
				health = trapManager.traps [i].health;
			}
		}
	}
		
	void OnTriggerEnter(Collider other){

		float distance = Vector3.Distance (other.gameObject.transform.position, transform.position);
		if(distance <= radius && other.gameObject.tag == "Enemy"){
			ApplyTrapEffect (other.gameObject);
		}
	}

	//What we do if this is a trap
	void ApplyTrapEffect(GameObject enemy){

		//Subtract damage from the enemy
		enemy.GetComponent<EnemyHealth>().TakeDamage(damage);

		//Degrade the trap
		health -= degradeAmount;
		if(CheckBroken()){
			Destroy (gameObject);
		}

		//Apply some effect to the enemy

	}

	bool CheckBroken(){
		if(health <= 0.0f){
			return true;
		}
		else{
			return false;
		}
	}
}
