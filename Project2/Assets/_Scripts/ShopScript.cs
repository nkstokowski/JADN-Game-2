using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour {

	Pause pauseManager;
    PlayerHealth playerHealth;
    PlayerMovement playerMovement;
    SwordAttack swordAttack;
    ArrowShooting arrowShooting;
    EnemyHealth enemyHealth;
    Blink blink;
    public GameObject shopCanvas;
	bool isShopOpen = false;
    int money = 10000;
    int costDmg = 100;
    int costSpd = 100;
    int costHealth = 100;
    int blinkCost = 100;
    int speedTier = 1;
    int healthTier = 1;
    int dmgTier = 1;
    int blinkTier = 1;



	void Start(){
		pauseManager = GameObject.Find ("Game_Manager").GetComponent<Pause>();
        playerHealth = GameObject.Find("NewPlayer").GetComponent<PlayerHealth>();
        playerMovement = GameObject.Find("NewPlayer").GetComponent<PlayerMovement>();
        swordAttack = GameObject.Find("SM_Katana").GetComponent<SwordAttack>();
        arrowShooting = GameObject.Find("NewPlayer").GetComponent<ArrowShooting>();
        blink = GameObject.Find("NewPlayer").GetComponent<Blink>();

        //money = 100000;

		shopCanvas.SetActive (false);
	}

	void Update(){
		CheckInputForShop ();
        increaseMoney();
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
		pauseManager.isPaused = true;
        shopCanvas.SetActive (true);
        Time.timeScale = 0;
    }

    public void CloseShop()
	{
		isShopOpen = false;
		pauseManager.isPaused = false;
        shopCanvas.SetActive (false);
        Time.timeScale = 1;
    }

    public void IncreaseHealth()
    {
        if (money >= costHealth)
        {
            playerHealth.maxHealth += 25;
            costHealth += 50;
            GameObject.Find("Health upgrade " + healthTier).SetActive(false);
            healthTier++;
            money -= costHealth;
        }
    }

    public void IncreaseDamage()
    {
        if (money >= costDmg)
        {
            arrowShooting.arrowDamage += 25;
            swordAttack.damage += 25;
            costDmg += 50;
            GameObject.Find("Attack Upgrade " + dmgTier).SetActive(false);
            dmgTier++;
            money -= costDmg;
        }
    }

    public void IncreaseSpeed()
    {
        if (money >= costSpd)
        {
            playerMovement.walkSpeed += .05f;
            costSpd += 50;
            GameObject.Find("Speed Upgrade " + speedTier).SetActive(false);
            speedTier++;
            money -= costSpd;
        }
    }

    public void IncreaseBlink()
    {
        if (money >= blinkCost)
        {
            blink.blinkDistance += .05f;
            blinkCost += 50;
            GameObject.Find("Blink Upgrade " + blinkTier).SetActive(false);
            blinkTier++;
            money -= blinkCost;
        }
    }

    void increaseMoney()
    {
        if (swordAttack.hit == true)
        {
            money += 25;
        }
    }
}
