using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

	public bool isPaused = false;

	public GameObject resumeButton;
	public GameObject menuButton;

	void Start(){
		resumeButton.SetActive(false);
		menuButton.SetActive(false);
	}

	public void TogglePaused(){
		isPaused = !isPaused;
	}

	public void LoadScene(string level){
		SceneManager.LoadScene(level);
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			TogglePaused();
		}
			
		if(isPaused){
			Time.timeScale = 0.0f;
			resumeButton.SetActive(true);
			menuButton.SetActive(true);
		} else{
			Time.timeScale = 1.0f;
			resumeButton.SetActive(false);
			menuButton.SetActive(false);
		}
	}
}
