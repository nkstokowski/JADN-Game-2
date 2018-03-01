using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooting : MonoBehaviour {

    // GameObjects or Components that the script is dependent on
    [HeaderAttribute("Dependencies")]
    public GameObject gameManagerObj;
    public Transform gunEnd;
    public GameObject arrow;
    private GameManager manager;
    private LineRenderer arrowLine;

    // Variables that affect the shooting
    [HeaderAttribute("Shooting Variables")]
    public int gunDamage = 1;
    public float fireRate = .25f;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    private float nextFire;
    private float shotDistance = 0.0f;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);

    [HeaderAttribute("Charge Shot Variables")]
    public float baseCharge = .1f;
    public float perfectCharge = .6f;
    public float chargeThreshold = .1f;
    public float chargeRate = .5f;
    private float charge = .1f;

    void Start()
    {
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

    void Update()
    {

        // inputActive is the state of the fire button
        // When singleFire is active, the button must be pressed. Otherwise it can be held
        // I don't know what would happen if singleFire were toggled on while firing
        //bool inputActive = (singleFire) ? Input.GetButtonDown("Fire1") : Input.GetButton("Fire1");

        if (Input.GetButton("Fire1"))
        {
            charge += (charge < 1.0f) ? chargeRate * Time.deltaTime : 0f;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            Debug.Log("You had: " + (perfectCharge - charge));
            // If fire is being pressed, the weapon is ready to fire, and the game is not paused, you can fire
            if (Time.time > nextFire && !manager.isPaused && (perfectCharge - charge) >= -chargeThreshold)
            {

                if(Mathf.Abs(perfectCharge - charge) <= chargeThreshold)
                {
                    shotDistance = weaponRange;
                    Debug.Log("Perfect!");
                }
                else
                {
                    shotDistance = charge * weaponRange;
                    //Debug.Log("Miss. You had: " + (perfectCharge - charge));
                }

                nextFire = Time.time + fireRate;

                StartCoroutine(ShotEffect());

                RaycastHit hit;
                arrowLine.SetPosition(0, gunEnd.position); // Sets the starting position of the lineSegment to the gun end

                // Check for ray collisions
                if (Physics.Raycast(gunEnd.position, gunEnd.forward, out hit, shotDistance))
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
                    arrowLine.SetPosition(1, gunEnd.position + (gunEnd.forward * shotDistance));
                }
            }
            charge = baseCharge;
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
