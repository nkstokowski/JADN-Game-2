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
}
