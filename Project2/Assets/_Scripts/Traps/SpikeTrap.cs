using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : BaseTrap
{

	public AudioClip effectCLip;

	void Start(){

        //Load our trap stats
        InitTrap();
    }
		
	void OnTriggerEnter(Collider other){
		//Debug.Log("Spike Trap");
		if(other.gameObject.tag == "Enemy"){
			ApplyTrapEffect(other.gameObject);
			Debug.Log("Spike Trap");
		}
	}

	//What we do if this is a trap
	void ApplyTrapEffect(GameObject enemy){

		GetComponent<AudioSource>().PlayOneShot(effectCLip);
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
