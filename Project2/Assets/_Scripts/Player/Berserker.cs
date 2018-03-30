using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	public GameObject countDownObject;
	Text countDownText;

	public GameObject effect;
	public GameObject effectSpawn;

	void Start(){
		range = player.GetComponent<ArrowShooting>();
		melee = sword.GetComponent<SwordAttack>();
		movement = player.GetComponent<PlayerMovement>();
		abilityManager = manager.GetComponent<AbilityManager>();
		countDownText = countDownObject.GetComponent<Text>();
		countDownText.enabled = false;
	}

	void Update(){
		if (Input.GetKeyDown (key) && canUseAbility) {
			//GameObject game = Instantiate (effect, effectSpawn.transform.position, Quaternion.identity);
			//(Instantiate (effect) as GameObject).transform.parent = effectSpawn.transform;
				
				

			StartCoroutine (TriggerAbility ());
		} 
	}

	public IEnumerator TriggerAbility(){
		canUseAbility = false;
		effect.transform.position = effectSpawn.transform.position;
		effect.transform.parent = effectSpawn.transform;
		StartCoroutine(CountDownText());
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
		yield return new WaitForSeconds (cooldown);
		canUseAbility = true;
		abilityManager.EnableAbilityImage(abilityManager.berserkserObject);
		StopCoroutine(CountDownText());
		countDownText.enabled = false;
	}

	IEnumerator CountDownText(){
		float totalTime = cooldown + abilityTime;
		int totalTimeFormat = (int)totalTime;
		countDownText.text = totalTimeFormat.ToString();
		countDownText.enabled = true;
		float tempNum = totalTime;

		while(tempNum >=0){
		yield return new WaitForSeconds(1);
		tempNum--;
		int display = (int)tempNum;
		countDownText.text = display.ToString();
			if (tempNum <= 3) {
				effect.transform.parent = null;
				effect.transform.position = new Vector3 (0, 0, 0);
			}
		}

	}


}
