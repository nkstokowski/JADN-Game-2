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
			TowerHealth tH = target.GetComponent<TowerHealth> ();
			//tH.TakeDamage (damage);
			return;
		}
		if(target.tag == "Player"){
			PlayerHealth pH = target.GetComponent<PlayerHealth> ();
			//pH.TakeDamage (damage);
			return;
		}
	}

}
