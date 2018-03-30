using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningArc : MonoBehaviour {

    private bool attacking = false;
    private float radius = 1.0f;
    private float damage = 1.0f;
    private GameObject target;
    private LineRenderer line;
    public float attackThreshold = 1.0f;

	// Use this for initialization
	void Start () {
        line = gameObject.GetComponent<LineRenderer>();
        line.SetPosition(0, transform.position);
        line.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (!target)
        {
            attacking = false;
            line.enabled = false;
            return;
        }

        // If target is ever lost, stop attacking
        if(Vector3.Distance(target.transform.position, transform.position) > (radius + attackThreshold))
        {
            attacking = false;
            line.enabled = false;
            target.GetComponent<EnemyAI>().setBeingAttacked(false);
            target = null;
        }

        // If you have a target, and it is within range, draw a bolt
        if (attacking)
        {
            line.SetPosition(1, target.transform.position);
            EnemyHealth health = target.GetComponent<EnemyHealth>();
            if (health)
            {
                health.TakeDamage(damage * Time.deltaTime);
            }
        }
	}

    public void initArc(float attackRadius, float attackDamage)
    {
        radius = attackRadius;
        damage = attackDamage;
    }

    // Set target and enable line
    public void attack(GameObject enemy)
    {
        target = enemy;
        attacking = true;
        line.SetPosition(0, transform.position);
        line.enabled = true;
        target.GetComponent<EnemyAI>().setBeingAttacked(true);
    }

    // return if attacking
    public bool isAttacking()
    {
        return attacking;
    }

    // return current target
    public GameObject myTarget()
    {
        return target;
    }
}
