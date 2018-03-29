using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTower : BaseTrap
{
    [HeaderAttribute("Dependencies")]
    public GameObject lightningArcObject;
    public GameObject AttackOrigin;

    public int arcCount = 3;
    private LightningArc[] arcs;

    void Start()
    {

        type = TrapType.Lightning;
        arcs = new LightningArc[arcCount];

        //Load our trap stats
        InitTrap();

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
        if (other.gameObject.tag == "Enemy" && active)
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
}
