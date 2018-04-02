using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingTower : BaseTrap {

    private GameObject Player;
    public int healAmount;
    private bool healing;
    private float nextTimeToHeal = 0f;
    public float threshold = 7.0f;
    private PlayerHealth playerHealth;

	// Use this for initialization
	void Start () {
        type = TrapType.Healing;

        //Load our trap stats
        InitTrap();

        gameObject.GetComponent<SphereCollider>().radius = radius;
        Player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = Player.GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && base.active)
        {
            healing = true;
        }
    }

    // Update is called once per frame
    void Update () {
        if (healing)
        {
            // Check distance for end of healing
            if(Vector3.Distance(Player.transform.position, transform.position) > threshold){
                healing = false;
            }
            else if(Time.time >= nextTimeToHeal)
            {
                playerHealth.Heal(healAmount);
                nextTimeToHeal = Time.time + fireRate;
            }
        }
	}
}
