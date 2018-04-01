using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour {

    public int damage = 34;
    public bool hit;
	AudioSource soundHit;
	// Use this for initialization
	void Start () {
		soundHit = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

        hit = false;

	}

    void OnTriggerEnter(Collider other)
    {
        // Detect collision with enemy
        if (other.gameObject.tag == "Enemy")
        {
			soundHit.Play ();
            EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();
            if (health)
            {
                Debug.Log("Sword Hit!");
                hit = true;
                health.TakeDamage(damage);
            }
        }
    }


}
