using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour {

    public Material normalTrail;
    public Material perfectTrail;
    private float speed = 10.0f;
    private Vector3 target;
    private bool fired = false;
    private int damage = 0;
    private int pierce = 1;

	void Start () {
		
	}
	
	void Update () {
        if (fired)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
	}


    // Set the configuration of the arrow
    public void fireArrow(Vector3 targetPoint, float arrowSpeed, int arrowDamage, int pierceCount, bool perfect)
    {
        target = targetPoint;
        speed = arrowSpeed;
        damage = arrowDamage;
        fired = true;
        pierce = pierceCount;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();
            if (health)
            {
                Debug.Log("Hit!");
                health.TakeDamage(damage);
                pierce--;
                // Don't destroy until it has pierced enough
                if (pierce <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
        else if(other.gameObject.tag != "Player")
        {
            fired = false;
        }
    }
}
