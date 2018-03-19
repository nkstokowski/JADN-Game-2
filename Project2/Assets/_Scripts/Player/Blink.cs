using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Blink : MonoBehaviour {

	public KeyCode abilityKey;
	public float blinkDistance = 2.5f;
	public float blinkThreshold = 2.5f;
	public float cooldown = 5.0f;
	bool canUseAbility = true;
	public GameObject characterObject;

	void Update(){
		if(Input.GetKeyUp(abilityKey) && canUseAbility){
			StartCoroutine (BeginCoolDown ());
			ApplyBlink ();
		} else {
			return;
		}
	}

	void ApplyBlink() {
		Vector3 blinkPosition = transform.position + (characterObject.transform.forward * blinkDistance);
		NavMeshHit hit;
		if(NavMesh.SamplePosition(blinkPosition, out hit,blinkThreshold,NavMesh.AllAreas)){
			transform.position = Vector3.Lerp (transform.position,blinkPosition, 1);
		} else {
			transform.position = Vector3.Lerp (transform.position,hit.position, 1);
		}
	}

	IEnumerator BeginCoolDown() {
		canUseAbility = false;
		yield return new WaitForSeconds (cooldown);
		canUseAbility = true;
	}

}
