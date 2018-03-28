using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	public GameObject manager;
	AbilityManager abilityManager;

	public GameObject countDownObject;
	Text countDownText;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rbody = GetComponent<Rigidbody>();
		abilityManager = manager.GetComponent<AbilityManager>();
		countDownText = countDownObject.GetComponent<Text>();
		countDownText.enabled = false;
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
		//Debug.Log ("Hi");		
		GameObject[] targets = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject o in targets) {
			if (InRange(o.transform.position)) 
			{
				enemy = o.GetComponent<EnemyHealth> ();
				o.GetComponent<Animator> ().SetBool ("bool1", true);
				o.GetComponent<Animator> ().Play ("Hit");
				//Debug.Log (enemy);
				enemy.currentHealth -= 50;
				//Destroy (o);
				o.GetComponent<Animator> ().SetBool ("bool1", false);
				enemy = o.GetComponent<EnemyHealth> ();	
				enemy.TakeDamage(damage);

			}
		}
	}
	IEnumerator BeginCoolDown() {
		StartCoroutine(CountDownText());
		abilityManager.DisableAbilityImage(abilityManager.aoeObject);
		use = false;
		yield return new WaitForSeconds (cooldown);
		use = true;
		abilityManager.EnableAbilityImage(abilityManager.aoeObject);
		StopCoroutine(CountDownText());
		countDownText.enabled = false;
	}

	void Attack()
	{
		Instantiate (ability, effectSpawn.transform.position, Quaternion.identity);
		DealDamage ();
	}

	IEnumerator CountDownText(){
		
		countDownText.text = cooldown.ToString();
		countDownText.enabled = true;
		float tempNum = cooldown;

		while(tempNum >=0){
		yield return new WaitForSeconds(1);
		tempNum--;
		int display = (int)tempNum;
		countDownText.text = display.ToString();
		}
	}
}
