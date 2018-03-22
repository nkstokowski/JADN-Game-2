using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // GameObjects or Components that the script is dependent on
    [HeaderAttribute("Dependencies")]
    public GameObject player;
    public GameObject gameManagerObj;
	private Pause gameManagerPause;


    // Variables that affect the movement of the player
    [HeaderAttribute("Movement Variables")]
	public float currentSpeed = 0.0f;
	public float walkSpeed = 10.0f;
	public float runSpeed;
	public bool isRunning = false;
    public Vector3 nextPosition;

    // Variables that affect the rotation of the player
    [HeaderAttribute("Rotation Variables")]
    public float cameraRadius = 0.001f;

    void Start(){
		currentSpeed = walkSpeed;
		runSpeed = walkSpeed * 1.5f;
        if (!gameManagerObj)
        {
			Debug.Log(name + ": No Game Manager Found");
        }
        else
        {
			gameManagerPause = gameManagerObj.GetComponent<Pause>();
        }
	}

	void Update(){

		//If we are paused, don't do any moving.
		if(gameManagerPause.isPaused){
			return;
		}

        Vector3 currentPosition = transform.position;

		// Player Movement
		var x = Input.GetAxis ("Horizontal") * Time.deltaTime * currentSpeed;
		var z = Input.GetAxis ("Vertical") * Time.deltaTime * currentSpeed;

		transform.Translate(x, 0.0f, z);
	}

	//Throwing the rotation in the FixedUpdate means it won't be called when TimeScale == 0.
	void FixedUpdate(){
		//Player rotation
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
