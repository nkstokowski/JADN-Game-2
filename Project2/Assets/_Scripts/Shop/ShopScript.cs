using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour {

	GameManager gameManager;
    PlayerMovement playerMovement;
    PlayerHealth playerHealth;
    ArrowShooting arrowShooting;
    EnemyHealth enemyHealth;
    public GameObject shopCanvas;
	bool isShopOpen = false;
    public int money = 0;

	void Start(){
		gameManager = GameObject.Find ("Game_Manager").GetComponent<GameManager>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        arrowShooting = GameObject.Find("Player").GetComponent<ArrowShooting>();
        enemyHealth = GameObject.Find("AIBot").GetComponent<EnemyHealth>();
		shopCanvas.SetActive (false);
	}

	void Update(){
		CheckInputForShop ();
	}

	void CheckInputForShop(){
		if(Input.GetKeyUp(KeyCode.B)){
			OpenShop ();
		}
		if(isShopOpen && Input.GetKeyUp(KeyCode.Escape)){
			CloseShop ();
		}
	}

    void OpenShop()
    {
		isShopOpen = true;
		gameManager.isPaused = true;
        shopCanvas.SetActive (true);
        Time.timeScale = 0;
    }

    public void CloseShop()
	{
		isShopOpen = false;
		gameManager.isPaused = false;
        shopCanvas.SetActive (false);
        Time.timeScale = 1;
    }

    public void increaseHealth()
    {
        playerHealth.health += 25;
    }

    public void increaseSpeed()
    {
        playerMovement.walkSpeed += 5;
    }

    public void increaseDamage()
    {
        arrowShooting.arrowDamage += 25;
    }

    public void gainMoney()
    {
        if(enemyHealth.die == true)
        {
            money += 100;
        }
    }
}
