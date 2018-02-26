using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour {

	GameManager gameManager;
    public GameObject shopCanvas;
	bool isShopOpen = false;

	void Start(){
		gameManager = GameObject.Find ("Game_Manager").GetComponent<GameManager>();
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
}
