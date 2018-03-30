using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : BaseTrap
{

	void Start(){

        //Load our trap stats
        InitTrap();
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
}
