using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public GameObject player;
	GameManager gameManager;

    [HeaderAttribute("Movement Variables")]
	public float currentSpeed = 0.0f;
	public float walkSpeed = 10.0f;
	public float runSpeed;
	public bool isRunning = false;
    public Vector3 nextPosition;

    [HeaderAttribute("Rotation Variables")]
    public float cameraRadius = 0.001f;

    void Start(){
		currentSpeed = walkSpeed;
		runSpeed = walkSpeed * 1.5f;
		gameManager = GameObject.Find ("Game_Manager").GetComponent<GameManager>();
	}

	void Update(){

		if(Input.GetKey(KeyCode.LeftShift)){
			currentSpeed = runSpeed;
			isRunning = true;
		}
		else{
			currentSpeed = walkSpeed;
			isRunning = false;
		}

        Vector3 currentPosition = transform.position;

		// Player Movement
		var x = Input.GetAxis ("Horizontal") * Time.deltaTime * currentSpeed;
		var z = Input.GetAxis ("Vertical") * Time.deltaTime * currentSpeed;

		transform.Translate(x, 0.0f, z);

		// Player rotation
		// Position of player
		if(!gameManager.isPaused){
			Vector2 playerOnScreen = Camera.main.WorldToViewportPoint (transform.position);

			// Position of Mouse
			Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

			if(Vector2.Distance(playerOnScreen, mouseOnScreen) >= cameraRadius)
			{
				// Angle between player and mouse
				float angle = Mathf.Atan2(playerOnScreen.y - mouseOnScreen.y, playerOnScreen.x - mouseOnScreen.x) * Mathf.Rad2Deg;

				// Rotate
				player.transform.rotation = Quaternion.Euler(new Vector3(0f, -angle - 90, 0f));
			}			
		}


	}
}
