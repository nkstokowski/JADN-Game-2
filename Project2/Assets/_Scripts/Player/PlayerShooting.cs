using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    // GameObjects or Components that the script is dependent on
    [HeaderAttribute("Dependencies")]
    public GameObject gameManagerObj;
    public Transform gunEnd;
    private GameManager manager;
    private LineRenderer arrowLine;

    // Variables that affect the shooting
    [HeaderAttribute("Shooting Variables")]
    public int gunDamage = 1;
    public float fireRate = .25f;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public bool singleFire;

    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private float nextFire;

	private GameManager gameManager;

	void Start () {
        arrowLine = GetComponent<LineRenderer>();
        if (!gameManagerObj)
        {
            Debug.Log(name + ": No Game Manager Found");
        }
        else
        {
            manager = gameManagerObj.GetComponent<GameManager>();
        }
    }
	
	void Update () {

        // inputActive is the state of the fire button
        // When singleFire is active, the button must be pressed. Otherwise it can be held
        // I don't know what would happen if singleFire were toggled on while firing
        bool inputActive = (singleFire) ? Input.GetButtonDown("Fire1") : Input.GetButton("Fire1");


        // If fire is being pressed, the weapon is ready to fire, and the game is not paused, you can fire
		if (inputActive && Time.time > nextFire && !manager.isPaused)
        {
            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());

            RaycastHit hit;
            arrowLine.SetPosition(0, gunEnd.position); // Sets the starting position of the lineSegment to the gun end

            // Check for ray collisions
            if(Physics.Raycast(gunEnd.position, gunEnd.forward, out hit, weaponRange))
            {
                // If collision detected, set the end of the lineSegment to the collision point
                arrowLine.SetPosition(1, hit.point);
                EnemyHealth health = hit.collider.GetComponent<EnemyHealth>();

                // If the thing that was hit has a health script, deal it some damage
                if (health)
                {
                    health.TakeDamage(gunDamage);
                }

                // If the thing that was hit has a rigidbody, push it? Probably will get taken out
                if (hit.rigidbody)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
            else
            {

                // If there was not hit, just draw the line to the end of the weapons range
                arrowLine.SetPosition(1, gunEnd.position + (gunEnd.forward * weaponRange));
            }

        }
	}


    // Co-routine for the effects surrounding the shot
    // Things like audio effects for the shots can be played here
    private IEnumerator ShotEffect()
    {
        arrowLine.enabled = true;
        yield return shotDuration;
        arrowLine.enabled = false;
    }


}
