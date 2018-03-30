using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTrap : MonoBehaviour {

    public GameObject trapManagerObject;
    protected bool active;
    protected TrapManager trapManager;
    protected float damage;
    protected float radius;
    protected float degradeAmount;
    protected float health;
    protected TrapType type;
    protected TrapEffect effect;
    protected bool hasRepeatedEffect;
    protected float fireRate;

    protected bool CheckBroken()
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

    protected void InitTrap()
    {
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
                hasRepeatedEffect = trapManager.traps[i].hasRepeatedEffect;
            }
        }

        active = false;
    }

    public void setActive(bool state)
    {
        active = state;
    }
}
