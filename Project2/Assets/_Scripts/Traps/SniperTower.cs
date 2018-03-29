using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTower : BaseTrap
{
    [HeaderAttribute("Dependencies")]
    public GameObject AttackOrigin;
    private LineRenderer bulletLine;

    [HeaderAttribute("Shooting Variables")]
    public WaitForSeconds shotDuration = new WaitForSeconds(.2f);
    private float nextTimeToFire = 0f;
    private bool readyToFire = true;

    void Start()
    {

        type = TrapType.Sniper;

        //Load our trap stats
        InitTrap();

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
        if (Time.time >= nextTimeToFire && active)
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
}
