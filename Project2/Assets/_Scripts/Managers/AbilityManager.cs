﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour {

	public GameObject blinkObject;
	public GameObject berserkserObject;
	public GameObject aoeObject;

	Image blinkImage;
	Image berserkserImage;
	Image aoeImage;

	public float disabledReduction = 0.5f;

	void Start(){
		blinkImage = blinkObject.GetComponent<Image>();
		berserkserImage = berserkserObject.GetComponent<Image>();
		aoeImage = aoeObject.GetComponent<Image>();
	}

	public void DisableAbilityImage(GameObject abilityImage){
		Image temp = abilityImage.GetComponent<Image>();
		temp.color = new Color(temp.color.r,temp.color.g,temp.color.b,disabledReduction);
	}

	public void EnableAbilityImage(GameObject abilityImage){
		Image temp = abilityImage.GetComponent<Image>();
		temp.color = new Color(temp.color.r,temp.color.g,temp.color.b,1.0f);
	}
}
