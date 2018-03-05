using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShootingRay : MonoBehaviour
{

    // GameObjects or Components that the script is dependent on
    [HeaderAttribute("Dependencies")]
    public ParticleSystem emitter;
    public GameObject gameManagerObj;
    public Transform gunEnd;
    private GameManager manager;

    // Variables that affect the shooting
    [HeaderAttribute("Shooting Variables")]
    public int gunDamage = 100;
    public float fireRate = .25f;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public float arrowSpeed = 50.0f;
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
            //Debug.Log("You had: " + (perfectCharge - charge));
            // If fire is being pressed, the weapon is ready to fire, and the game is not paused, you can fire
            if (Time.time > nextFire && !manager.isPaused && (perfectCharge - charge) >= -chargeThreshold)
            {

                if (Mathf.Abs(perfectCharge - charge) <= chargeThreshold)
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

                emitter.Emit(1);

                // Check for ray collisions
                if (Physics.Raycast(gunEnd.position, gunEnd.forward, out hit, shotDistance))
                {
                    // If collision detected, set the end of the lineSegment to the collision point
                    EnemyHealth health = hit.collider.GetComponent<EnemyHealth>();

                    // If the thing that was hit has a health script, deal it some damage
                    if (health)
                    {
                        //Debug.Log("Hit!");
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
                }
            }
            charge = baseCharge;
        }
    }


    // Co-routine for the effects surrounding the shot
    // Things like audio effects for the shots can be played here
    private IEnumerator ShotEffect()
    {
        yield return shotDuration;
    }


}
