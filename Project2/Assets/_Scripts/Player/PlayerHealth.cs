using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	public int health = 100;

	public void TakeDamage(int damage){
		health -= damage;
		if(CheckForDeath()){
			//End Game
		}
		else{
			return;
		}
	}

	private bool CheckForDeath(){
		if(health <= 0){
			return true;
		}
		else{
			return false;
		}
	}
}
