using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public int health = 100;
    public int maxHealth = 100;
	public GameObject manager;
	public GameObject healthSlider;

	public void TakeDamage(int damage){
		Debug.Log ("Taking Damage!");
		health -= damage;
		int score = manager.GetComponent<ScoreManager>().playerScore;
		if(CheckForDeath()){
			PlayerPrefs.SetInt("playerScore", manager.GetComponent<ScoreManager>().playerScore);
			SceneManager.LoadScene("GameOver");
		}
		else{
			return;
		}
	}

    public void Heal(int heal)
    {
        health += heal;
        if(health > maxHealth)
        {
            health = maxHealth;
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
}
