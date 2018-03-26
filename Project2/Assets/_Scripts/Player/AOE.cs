using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE : MonoBehaviour {

	public KeyCode key;
	public GameObject ability;
	public Animator anim;
	public Rigidbody rbody;
	public EnemyHealth enemy;
	public GameObject effectSpawn;
	public float cooldown = 5.0f;
	public int damage = 50;
	bool use = true;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (key) && use) {
			//Instantiate (ability, transform.position, Quaternion.identity);
			DealDamage ();

			StartCoroutine(BeginCoolDown());
			anim.Play ("Attack_04", -1, 0F);
			Invoke ("Attack", 0.7f);
		}
	}

	public bool InRange(Vector3 enemy)
	{
		if (Vector3.SqrMagnitude (enemy - transform.position) <= 30) 
		{
			return true;
		}
		else {
			return false;
		}
	}
	public void DealDamage()
	{
		GameObject[] targets = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject o in targets) {
			if (InRange(o.transform.position)) 
			{
				enemy = o.GetComponent<EnemyHealth> ();	
				enemy.TakeDamage(damage);
			}
		}
	}
	IEnumerator BeginCoolDown() {
		use = false;
		yield return new WaitForSeconds (cooldown);
		use = true;
	}

	void Attack()
	{
		Instantiate (ability, effectSpawn.transform.position, Quaternion.identity);
		DealDamage ();
	}
}
