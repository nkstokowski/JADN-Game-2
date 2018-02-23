using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[HeaderAttribute("Movement Variables")]
	public float currentSpeed = 0.0f;
	public float walkSpeed = 5.0f;
	public float runSpeed;
	public bool isRunning = false;



	void Start(){
		currentSpeed = walkSpeed;
		runSpeed = walkSpeed * 1.5f;
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

		//Player Movement
		var x = Input.GetAxis ("Horizontal") * Time.deltaTime * currentSpeed;
		var z = Input.GetAxis ("Vertical") * Time.deltaTime * currentSpeed;
		transform.Translate(x, 0.0f, z);

		//Player rotation
		//Get the Screen positions of the object
		Vector2 positionOnScreen = Camera.main.WorldToViewportPoint (transform.position);

		//Get the Screen position of the mouse
		Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

		//Get the angle between the points
		float angle = Mathf.Atan2(positionOnScreen.y - mouseOnScreen.y, positionOnScreen.x - mouseOnScreen.x) * Mathf.Rad2Deg;

		//Ta Daaa
		transform.rotation =  Quaternion.Euler (new Vector3(0f,-angle - 90,0f));

		
	}
}
