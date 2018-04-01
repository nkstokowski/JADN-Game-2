using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Blink : MonoBehaviour {

	public KeyCode abilityKey;
	public float blinkDistance = 2.5f;
	public float blinkThreshold = 2.5f;
	public float cooldown = 5.0f;
	bool canUseAbility = true;
	public GameObject characterObject;
	public GameObject manager;
	AbilityManager abilityManager;
	public GameObject countDownObject;
	Text countDownText;

    AudioSource[] sounds;
    AudioSource warp;

    void Start()
    {
        sounds = gameObject.GetComponents<AudioSource>();
        warp = sounds[2];

        abilityManager = manager.GetComponent<AbilityManager>();
		countDownText = countDownObject.GetComponent<Text>();
		countDownText.enabled = false;
	}

	void Update(){
		if(Input.GetKeyUp(abilityKey) && canUseAbility){
			StartCoroutine (BeginCoolDown ());
			ApplyBlink ();
            warp.Play();
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
		StartCoroutine(CountDownText());
		abilityManager.DisableAbilityImage(abilityManager.blinkObject);
		canUseAbility = false;
		yield return new WaitForSeconds (cooldown);
		canUseAbility = true;
		abilityManager.EnableAbilityImage(abilityManager.blinkObject);
		StopCoroutine(CountDownText());
		countDownText.enabled = false;
	}

	IEnumerator CountDownText(){
		countDownText.text = cooldown.ToString();
		countDownText.enabled = true;
		float tempNum = cooldown;

		while(tempNum >=0){
		yield return new WaitForSeconds(1);
		tempNum--;
		int display = (int)tempNum;
		countDownText.text = display.ToString();
		}
	}

}
