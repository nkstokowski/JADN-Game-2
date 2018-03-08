using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	[HeaderAttribute("Enemy Type Variables")]
	public EnemyType type;
	int damage;
	float attackRadius;

	void Start(){
		damage = type.damage;
		attackRadius = type.attackRadius;
	}

	public void attack(GameObject target){
		if(target.tag == "Target"){
			Debug.Log ("Attacking Tower");
			target.GetComponent<TowerHealth> ().TakeDamage (damage);
			Destroy (gameObject);
			return;
		}
		if(target.tag == "Player"){
			target.GetComponent<PlayerHealth> ().TakeDamage (damage);
			return;
		}
	}

}
