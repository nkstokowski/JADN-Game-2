using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour {

	Pause pauseManager;
    public GameObject shopCanvas;
	bool isShopOpen = false;

	void Start(){
		pauseManager = GameObject.Find ("Game_Manager").GetComponent<Pause>();
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
}
