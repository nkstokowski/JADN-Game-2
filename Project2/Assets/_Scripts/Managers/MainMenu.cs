using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void LoadScene(string name){
		Debug.Log("Load game scene");
		SceneManager.LoadScene(name);
	}

	public void Quit(){
		Application.Quit();
	}
}
