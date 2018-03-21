﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour {

    public int damage = 34;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		


	}

    void OnTriggerEnter(Collider other)
    {
        // Detect collision with enemy
        if (other.gameObject.tag == "Enemy")
        {
            EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();
            if (health)
            {
                Debug.Log("Sword Hit!");
                health.TakeDamage(damage);
            }
        }
    }


}