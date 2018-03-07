using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * IMPORTANT: Make sure to set the GameManager variable of the Trap when spawning it in.
*/

public class TrapManager : MonoBehaviour {
	[HeaderAttribute("Trap Type Variables")]
	public Trap[] traps;
}

public enum TrapType{
	Spike,
	Tower,
    Lightning
}

public enum TrapEffect{
	AOE,
	Impact
}



[System.Serializable]
public struct Trap{
	public TrapType type;
	public TrapEffect effect;
	public int damage;
	public float radius;
	public int health;
	public bool hasRepeatedEffect;
	public float fireRate;
	public float degradeAmount;

	public Trap(TrapType typ, TrapEffect eff ,int dam, float rad, int h, bool hRE, float fR, float dA){
		type = typ;
		effect = eff;
		damage = dam;
		radius = rad;
		health = h;
		hasRepeatedEffect = hRE;
		fireRate = fR;
		degradeAmount = dA;
	}
}