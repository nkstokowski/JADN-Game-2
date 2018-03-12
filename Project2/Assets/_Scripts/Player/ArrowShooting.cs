using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowShooting : MonoBehaviour {

    // GameObjects or Components that the script is dependent on
    [HeaderAttribute("Dependencies")]
    public GameObject gameManagerObj;
    public GameObject arrowObject;
    public Slider chargeMeter;
    private Transform gunEnd;
    private Image chargeFill;
    private Pause pauseManager;
    private LineRenderer arrowLine;

    // Variables that affect the shooting
    [HeaderAttribute("Shooting Variables")]
    public int arrowDamage = 100;
    public float fireRate = .25f;
    public float weaponRange = 50f;
    public float arrowSpeed = 50.0f;
    public int pierceCount = 1;
    private float nextFire;
    private float shotDistance = 0.0f;
    private float shotSpeed = 50.0f;
    private bool perfectShot = false;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);

    [HeaderAttribute("Charge Shot Variables")]
    public float baseCharge = .1f;
    public float perfectCharge = .6f;
    public float chargeThreshold = .1f;
    public float chargeRate = .5f;
    private float charge = .1f;
    public Color chargeFillColor = Color.green;
    public Color perfectChageColor = Color.white;
    public Color failedChargeColor = Color.red;

    void Start()
    {
        if (!gameManagerObj)
        {
            Debug.Log(name + ": No Game Manager Found");
        }
        else
        {
			pauseManager = gameManagerObj.GetComponent<Pause>();
        }

        gunEnd = transform.Find("Cylinder").Find("Sphere");
        Transform fillArea = chargeMeter.transform.Find("Fill Area");
        chargeFill = fillArea.Find("Fill").GetComponent<Image>();
    }

    void Update()
    {
        // Reset boolean for firing a perfect shot
        perfectShot = false;

        // If the user is holding the button down
        if (Input.GetButton("Fire2"))
        {
            // Increase the charge
            charge += (charge < 1.0f) ? chargeRate * Time.deltaTime : 0f;

            // Display the charge meter and update its value
            chargeMeter.gameObject.SetActive(true);
            chargeMeter.value = charge / (perfectCharge - chargeThreshold);

            // If the charge is too great, set the meter to failed
            if ((perfectCharge - charge) < -chargeThreshold)
            {
                chargeFill.color = failedChargeColor;
            }
        }

        // If the user releases the button
        if (Input.GetButtonUp("Fire2"))
        {
            // Check the time to see if you can fire. Also check if the user has charged too much
			if (Time.time > nextFire && !pauseManager.isPaused && (perfectCharge - charge) >= -chargeThreshold)
            {

                // Check if the charge is within the limits to be considered perfect
                if(Mathf.Abs(perfectCharge - charge) <= chargeThreshold)
                {
                    // On a perfect shot, the distance is at max and the speed is doubled
                    shotDistance = weaponRange;
                    shotSpeed = arrowSpeed * 2.0f;
                    chargeFill.color = perfectChageColor;
                    perfectShot = true;
                    Debug.Log("Perfect!");
                }
                else
                {
                    // On a regular shot, the current charge determines the range
                    shotDistance = charge * weaponRange;
                    shotSpeed = arrowSpeed;
                }

                nextFire = Time.time + fireRate;

                StartCoroutine(ShotEffect());

                // Set some more perfect shot variables. Eventually these will all be set in the same place
                int damage = (perfectShot) ? arrowDamage * 2 : arrowDamage;
                int pierce = (perfectShot) ? pierceCount * 2 : pierceCount;

                // Create the arrow and fire it
                GameObject arrow = Instantiate(arrowObject, transform.position, transform.rotation);
                ArrowMovement arrowMV = arrow.GetComponent<ArrowMovement>();
                arrowMV.fireArrow((gunEnd.position + (gunEnd.forward * shotDistance)), shotSpeed, damage, pierce, perfectShot);
            }

            // Reset the charge
            charge = baseCharge;


            // Reset the charge meter
            chargeMeter.gameObject.SetActive(false);
            chargeMeter.value = 0;
            chargeFill.color = chargeFillColor;
        }
    }


    // Co-routine for the effects surrounding the shot
    // Things like audio effects for the shots can be played here
    private IEnumerator ShotEffect()
    {
        yield return shotDuration;
    }

   

}
