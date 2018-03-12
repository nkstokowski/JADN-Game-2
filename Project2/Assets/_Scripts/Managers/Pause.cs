using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

	public bool isPaused = false;

	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			isPaused = !isPaused;
		}
			
		if(isPaused){
			Time.timeScale = 0.0f;
		} else{
			Time.timeScale = 1.0f;
		}
	}
}
