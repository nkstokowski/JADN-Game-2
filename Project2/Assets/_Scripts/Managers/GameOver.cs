using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	int score = 0;
	public GameObject scoreText;
	public GameObject highScoreText;
	public GameObject highScoreMessage;
	void Start(){
		score = PlayerPrefs.GetInt("playerScore");
		highScoreMessage.SetActive(false);
		if(CheckForHighScore(score)){
			SetHighScore(score);
		}
		SetScores();
	}

	bool CheckForHighScore(int score){
		if(score > PlayerPrefs.GetInt("highScore"))
			return true;
		else
			return false;
	}

	void SetHighScore(int score){
		PlayerPrefs.SetInt("highScore",score);
		highScoreMessage.SetActive(true);
	}

	void SetScores(){
		scoreText.GetComponent<Text>().text = "Score: " + PlayerPrefs.GetInt("playerScore").ToString();
		highScoreText.GetComponent<Text>().text = "High Score: " + PlayerPrefs.GetInt("highScore").ToString();
	}

	public void SetScene(string name){
		SceneManager.LoadScene(name);
	}

	public void Reset(){
		PlayerPrefs.SetInt("highScore",0);
		SetScores();
	}
}
