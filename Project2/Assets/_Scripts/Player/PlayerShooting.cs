using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    public int gunDamage = 1;
    public float fireRate = .25f;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public Transform gunEnd;
    public bool singleFire;

    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private LineRenderer arrowLine;
    private float nextFire;

	// Use this for initialization
	void Start () {
        arrowLine = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        bool inputActive = (singleFire) ? Input.GetButtonDown("Fire1") : Input.GetButton("Fire1");

        if (inputActive && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());

            RaycastHit hit;
            arrowLine.SetPosition(0, gunEnd.position);

            if(Physics.Raycast(gunEnd.position, gunEnd.forward, out hit, weaponRange))
            {
                arrowLine.SetPosition(1, hit.point);
                EnemyHealth health = hit.collider.GetComponent<EnemyHealth>();
                if (health)
                {
                    health.TakeDamage(gunDamage);
                }

                if (hit.rigidbody)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
            else
            {
                arrowLine.SetPosition(1, gunEnd.position + (gunEnd.forward * weaponRange));
            }

        }
	}

    private IEnumerator ShotEffect()
    {
        arrowLine.enabled = true;
        yield return shotDuration;
        arrowLine.enabled = false;
    }


}
