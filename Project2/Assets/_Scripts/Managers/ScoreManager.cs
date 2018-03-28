using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public int playerScore = 0;
	public int playerMoney = 0;
	public Text playerMoneyText;
	public Text scoreText;

	void Update(){
		playerMoneyText.text = playerMoney.ToString();
		scoreText.text = "Score: " + playerScore.ToString();
	}

}
