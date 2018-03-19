using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
	public Animator anim;
	public Rigidbody rbody;

	private float inputH;
	private float inputV;
	private bool run;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
		rbody = GetComponent<Rigidbody>();
		run = false;
	}


	// Update is called once per frame
	void Update ()
	{
		/*
			if (Input.GetKeyDown ("1")) {
				anim.Play ("Attack_01", -1, 0F);
			}
			if (Input.GetKeyDown ("2")) {
				anim.Play ("Attack_02", -1, 0F);
			}
			if (Input.GetKeyDown ("3")) {
				anim.Play ("Attack_03", -1, 0F);
			}
			if (Input.GetKeyDown ("4")) {
				anim.Play ("Attack_04", -1, 0F);
			}
			
			if (Input.GetKeyDown ("5")) {
				anim.Play ("Attack_05", -1, 0F);
			}
			if (Input.GetKeyDown ("6")) {
				anim.Play ("Attack_06", -1, 0F);
			}
			if (Input.GetKeyDown ("7")) {
				anim.Play ("Attack_07", -1, 0F);
			}
			if (Input.GetKeyDown ("8")) {
				anim.Play ("Death_01", -1, 0F);
			}
			if (Input.GetKeyDown ("9")) {
				anim.Play ("Death_02", -1, 0F);
			}
			if (Input.GetKeyDown ("0")) {
			anim.Play ("Idle_nonWeapon", -1, 0F);
			}
			
			if (Input.GetKeyDown ("g")) {
				anim.Play ("Crouch", -1, 0F);
			}
			if (Input.GetKeyDown ("t")) {
				anim.Play ("Crouch_Move_F", -1, 0F);
			}
			if (Input.GetKeyDown ("r")) {
				anim.Play ("Crouch_Move_L", -1, 0F);
			}
			if (Input.GetKeyDown ("y")) {
				anim.Play ("Crouch_Move_R", -1, 0F);
			}
			*/

		if (Input.GetMouseButtonDown (0)) {
			anim.Play ("Attack_01", -1, 0F);
		}

		if (Input.GetMouseButton (1)) {
			anim.Play ("Attack_07", -1, 0.33F);
		}

		if(Input.GetKey(KeyCode.LeftShift)) 
		{
			run = true;
		}
		else
		{
			run = false;
		}

		if (Input.GetKey (KeyCode.Space)) 
		{
			anim.SetBool ("jump", true);
		} 
		else
		{
			anim.SetBool ("jump",false);
		}

		Vector2 playerOnScreen = Camera.main.WorldToViewportPoint (transform.position);
		Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
		float angle = Mathf.Atan2(playerOnScreen.y - mouseOnScreen.y, playerOnScreen.x - mouseOnScreen.x) * Mathf.Rad2Deg;
		//Debug.Log (angle);
		if (angle >= -135f && angle <= -45f) {
			//Debug.Log ("Reached");
			inputH = Input.GetAxis ("Horizontal");
			inputV = Input.GetAxis ("Vertical");
		}

		else if (angle >= -45 && angle <= 45) {
			inputH = Input.GetAxis ("Vertical Invert");
			inputV = Input.GetAxis ("Horizontal Invert");
		}

		else if (angle >= 45f && angle <= 135f) {
			//Debug.Log ("Reached");
			inputH = Input.GetAxis ("Horizontal Invert");
			inputV = Input.GetAxis ("Vertical Invert");
		}

		else {
			inputH = Input.GetAxis ("Vertical");
			inputV = Input.GetAxis ("Horizontal");
		}


		anim.SetFloat("inputH",inputH);
		anim.SetFloat("inputV",inputV);
		anim.SetBool("run",run);




		//rbody.velocity = new Vector3(moveX,0f,moveZ);
	}
}


