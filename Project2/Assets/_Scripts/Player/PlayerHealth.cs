﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public int health = 100;
    public int maxHealth;
	public GameObject manager;
	public GameObject healthSlider;

	public void TakeDamage(int damage){
		Debug.Log ("Taking Damage!");
		health -= damage;
		int score = manager.GetComponent<ScoreManager>().playerScore;
		if(CheckForDeath()){
			if(CheckForNewHighScore(score)){
				PlayerPrefs.SetInt("playerScore", manager.GetComponent<ScoreManager>().playerScore);
			}
			SceneManager.LoadScene("MainMenu");
		}
		else{
			return;
		}
	}

	void Update(){
		healthSlider.GetComponent<Slider>().value = health;
	}

	private bool CheckForDeath(){
		if(health <= 0){
			return true;
		}
		else{
			return false;
		}
	}

	private bool CheckForNewHighScore(int score){
		if(manager.GetComponent<ScoreManager>().playerScore > PlayerPrefs.GetInt("playerScore")){
			return true;
		}
		else{
			return false;
		}
	}


}
