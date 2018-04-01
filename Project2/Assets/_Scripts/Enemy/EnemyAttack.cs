using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	[HeaderAttribute("Enemy Type Variables")]
	public EnemyType type;
	int damage;
	float attackRadius;
	AudioSource soundHit;
	void Start(){
		damage = type.damage;
		attackRadius = type.attackRadius;
		soundHit = gameObject.GetComponent<AudioSource> ();
	}

	public void attack(GameObject target){
		if(target.tag == "Target"){
			soundHit.Play ();
			TowerHealth tH = target.GetComponent<TowerHealth> ();
			tH.TakeDamage(damage);
			return;
		}
		if(target.tag == "Player"){
			soundHit.Play ();
			PlayerHealth pH = target.GetComponent<PlayerHealth> ();
		
			//Debug.Log (pH.health);
			pH.TakeDamage(5);

			return;
		}
	}

}
