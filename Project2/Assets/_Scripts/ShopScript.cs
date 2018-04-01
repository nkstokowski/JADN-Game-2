using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour {

	Pause pauseManager;
    PlayerHealth playerHealth;
    PlayerMovement playerMovement;
    SwordAttack swordAttack;
    ArrowShooting arrowShooting;
    EnemyHealth enemyHealth;
    Blink blink;
    ScoreManager scoreManager;
    TowerPlacement towerPlacement;
    public Slider healthSlider;
    public GameObject slider;
    public GameObject spikeTrap;
    public GameObject lightning;
    public GameObject healthTower;
    float sliderWidth;
    public GameObject shopCanvas;
	bool isShopOpen = false;
    int costDmg = 100;
    int costSpd = 100;
    int costHealth = 100;
    int blinkCost = 100;
    int towerCost = 200;
    int speedTier = 1;
    int healthTier = 1;
    int dmgTier = 1;
    int blinkTier = 1;

	AudioSource[] sounds;
	AudioSource purchase;
    AudioSource upgrade;

	void Start(){
		sounds = gameObject.GetComponents<AudioSource> ();
		purchase = sounds [0];
        upgrade = sounds[2];
		pauseManager = GameObject.Find ("Game_Manager").GetComponent<Pause>();
        scoreManager = GameObject.Find("Game_Manager").GetComponent<ScoreManager>();
        towerPlacement = GameObject.Find("Game_Manager").GetComponent<TowerPlacement>();
        playerHealth = GameObject.Find("NewPlayer").GetComponent<PlayerHealth>();
        playerMovement = GameObject.Find("NewPlayer").GetComponent<PlayerMovement>();
        swordAttack = GameObject.Find("SM_Katana").GetComponent<SwordAttack>();
        arrowShooting = GameObject.Find("NewPlayer").GetComponent<ArrowShooting>();
        blink = GameObject.Find("NewPlayer").GetComponent<Blink>();
        sliderWidth = slider.GetComponent<RectTransform>().rect.width;


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
        if (scoreManager.playerMoney >= costHealth)
        {
            upgrade.Play();
			//purchase.Play ();
            playerHealth.maxHealth += 25;
            healthSlider.maxValue += 25;
            slider.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sliderWidth += 25);
            costHealth += 50;
            GameObject.Find("Health upgrade " + healthTier).SetActive(false);
            healthTier++;
            scoreManager.playerMoney -= costHealth;
        }
    }

    public void IncreaseDamage()
    {
        if (scoreManager.playerMoney >= costDmg)
        {
            upgrade.Play();
            //purchase.Play ();
            arrowShooting.arrowDamage += 25;
            swordAttack.damage += 25;
            costDmg += 50;
            GameObject.Find("Attack Upgrade " + dmgTier).SetActive(false);
            dmgTier++;
            scoreManager.playerMoney -= costDmg;
        }
    }

    public void IncreaseSpeed()
    {
        if (scoreManager.playerMoney >= costSpd)
        {
            upgrade.Play();
            //purchase.Play ();
            playerMovement.walkSpeed += .05f;
            costSpd += 50;
            GameObject.Find("Speed Upgrade " + speedTier).SetActive(false);
            speedTier++;
            scoreManager.playerMoney -= costSpd;
        }
    }

    public void IncreaseBlink()
    {
        if (scoreManager.playerMoney >= blinkCost)
        {
            upgrade.Play();
            //purchase.Play ();
            blink.blinkDistance += .05f;
            blinkCost += 50;
            GameObject.Find("Blink Upgrade " + blinkTier).SetActive(false);
            blinkTier++;
            scoreManager.playerMoney -= blinkCost;
        }
    }

    public void purchaseTower()
    {
        if (scoreManager.playerMoney >= towerCost)
        {
			purchase.Play ();
            towerPlacement.startPlacing(towerPlacement.towerObjectPrefab);
            scoreManager.playerMoney -= towerCost;
        }
    }

    public void purchaseSpike()
    {
        if (scoreManager.playerMoney >= towerCost)
        {
			purchase.Play ();
            towerPlacement.startPlacing(spikeTrap);
            scoreManager.playerMoney -= towerCost;
        }
    }

    public void purchaseLightning()
    {
        if (scoreManager.playerMoney >= towerCost)
        {
			purchase.Play ();
            towerPlacement.startPlacing(lightning);
            scoreManager.playerMoney -= towerCost;
        }
    }

    public void purchaseHealthTower()
    {
        if (scoreManager.playerMoney >= towerCost)
        {
            purchase.Play();
            towerPlacement.startPlacing(healthTower);
            scoreManager.playerMoney -= towerCost;
        }
    }
}
