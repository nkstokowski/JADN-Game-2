using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTower : MonoBehaviour
{
    [HeaderAttribute("Dependencies")]
    public GameObject trapManagerObject;
    public GameObject lightningArcObject;
    public GameObject AttackOrigin;
    TrapManager trapManager;


    float damage;
    float radius;
    float degradeAmount;
    float health;
    TrapType type = TrapType.Lightning;

    public int arcCount = 3;
    private LightningArc[] arcs;

    void Start()
    {

        arcs = new LightningArc[arcCount];

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
            }
        }

        // Make Lightning arcs
        gameObject.GetComponent<SphereCollider>().radius = radius;
        for(int i=0; i < arcCount; i++)
        {
            GameObject temp = Instantiate(lightningArcObject, AttackOrigin.transform.position, transform.rotation);
            arcs[i] = temp.GetComponent<LightningArc>();
            arcs[i].initArc(radius, damage);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            ApplyTrapEffect(other.gameObject);
        }
    }

    //What we do if this is a trap
    void ApplyTrapEffect(GameObject enemy)
    {

        EnemyAI ai = enemy.GetComponent<EnemyAI>();

        for (int i = 0; i < arcCount; i++)
        {
            if (!arcs[i].isAttacking() && !ai.isBeingAttacked())
            {
                arcs[i].attack(enemy);
            }
        }

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
