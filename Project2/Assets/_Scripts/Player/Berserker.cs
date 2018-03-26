using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserker : MonoBehaviour {

	ArrowShooting range;
	SwordAttack melee;
	PlayerMovement movement;

	public bool canUseAbility;
	public KeyCode key;
	public float abilityTime;
	public float cooldown;
	public GameObject sword;
	public GameObject player;
	public GameObject manager;
	AbilityManager abilityManager;

	void Start(){
		range = player.GetComponent<ArrowShooting>();
		melee = sword.GetComponent<SwordAttack>();
		movement = player.GetComponent<PlayerMovement>();
		abilityManager = manager.GetComponent<AbilityManager>();
	}

	void Update(){
		if(Input.GetKeyDown(key) && canUseAbility){
			StartCoroutine(TriggerAbility());
		}
	}

	public IEnumerator TriggerAbility(){
			abilityManager.DisableAbilityImage(abilityManager.berserkserObject);
			int storedRangeDamage = range.arrowDamage;
			int storedMeleeDamage = melee.damage;
			range.arrowDamage *= (int)1.5f;
			melee.damage *= (int)1.5f;
			movement.currentSpeed = movement.runSpeed;
			yield return new WaitForSeconds(abilityTime);
			range.arrowDamage = storedRangeDamage;
			melee.damage = storedMeleeDamage;
			movement.currentSpeed = movement.walkSpeed;
			StartCoroutine(BeginCoolDown());
	}

	IEnumerator BeginCoolDown() {
		canUseAbility = false;
		yield return new WaitForSeconds (cooldown);
		canUseAbility = true;
		abilityManager.EnableAbilityImage(abilityManager.berserkserObject);
	}


}
