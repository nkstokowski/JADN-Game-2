using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewTowerHealth : MonoBehaviour {

	public int health = 100;
	public GameObject manager;

	public void TakeDamage(int damage){
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
