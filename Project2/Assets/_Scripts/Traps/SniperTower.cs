using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTower : MonoBehaviour
{
    [HeaderAttribute("Dependencies")]
    public GameObject trapManagerObject;
    public GameObject AttackOrigin;
    private LineRenderer bulletLine;
    TrapManager trapManager;

    float damage;
    float radius;
    float degradeAmount;
    float health;
    TrapType type = TrapType.Sniper;

    [HeaderAttribute("Shooting Variables")]
    public WaitForSeconds shotDuration = new WaitForSeconds(.2f);
    private float nextTimeToFire = 0f;
    private float fireRate;
    private bool readyToFire = true;

    void Start()
    {

        //Load our trap stats

        trapManager = trapManagerObject.GetComponent<TrapManager>();
        for (int i = 0; i < trapManager.traps.Length; i++)
        {
            if (trapManager.traps[i].type == type)
            {
                //Load stats
                damage = trapManager.traps[i].damage;
                radius = trapManager.traps[i].radius;
                degradeAmount = trapManager.traps[i].degradeAmount;
                health = trapManager.traps[i].health;
                fireRate = trapManager.traps[i].fireRate;
            }
        }

        gameObject.GetComponent<SphereCollider>().radius = radius;
        bulletLine = GetComponent<LineRenderer>();
        //Debug.Log("My radius: " + radius);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //ApplyTrapEffect(other.gameObject);
        }
    }

    void Update()
    {
        if (Time.time >= nextTimeToFire)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
            readyToFire = true;
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (readyToFire && hitColliders[i].tag == "Enemy")
                {
                    ApplyTrapEffect(hitColliders[i].gameObject);
                }
            }
        }
    }

    //What we do if this is a trap
    void ApplyTrapEffect(GameObject enemy)
    {
        Vector3 heading = enemy.transform.position - transform.position;
        bulletLine.SetPosition(0, AttackOrigin.transform.position);

        RaycastHit hit;
        if (Physics.Raycast(AttackOrigin.transform.position, heading, out hit, radius * 2))
        {
            EnemyHealth health = hit.collider.GetComponent<EnemyHealth>();
            if (health)
            {
                Debug.Log("Hit!");
                readyToFire = false;
                nextTimeToFire = Time.time + fireRate;
                health.TakeDamage(damage);
                StartCoroutine(ShotEffect());
                bulletLine.SetPosition(1, hit.point);
            }
        }
    }

    private IEnumerator ShotEffect()
    {
        bulletLine.enabled = true;
        yield return shotDuration;
        bulletLine.enabled = false;
    }

    bool CheckBroken()
    {
        if (health <= 0.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
