using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
 * Author: Juri Kiin 2018 - jak5125@rit.edu
 * Game: Project 2 - GDD 2
 * Team: JADN Smith
 * Last Modified: 2/23/18
*/

public class EnemyAI : MonoBehaviour {

	NavMeshAgent agent;
	EnemyAttack enemyAttack;
	GameObject player;
	public GameObject target;
	TowerHealth towerHealth;// = target.GetComponent<TowerHealth>();
	PlayerHealth playerHealth;
	GameObject currentTarget;
    private bool beingAttacked = false;

	[HeaderAttribute("Movement Variables")]
	public float walkSpeed = 5.0f;
	public float chaseSpeed = 6.0f;
	public float stopDistance;
	public bool canSeePlayer = false;

	[HeaderAttribute("Detection Variables")]
	public float lineOfSightThreshold = 50.0f;
	public int fovLeft = -45;
	public int fovRight = 45;

	//Animation
	public Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();					//Get components needed for nav Mesh
		player = GameObject.FindGameObjectWithTag("Player");	//Get Player so we can save it as a reference
		target = GameObject.FindGameObjectWithTag ("Target");
		towerHealth = target.GetComponent<TowerHealth>();
		playerHealth = player.GetComponent<PlayerHealth> ();
		enemyAttack = GetComponent<EnemyAttack> ();
		currentTarget = target;

		//Set properties of the NavMeshAgent component. (This is so we only have to touch this compenent not mess with the component in the inspector itself.)
		agent.speed = walkSpeed;
		agent.stoppingDistance = stopDistance;
		SetTarget (target);

		//Warp
		NavMeshHit hit;
		if(NavMesh.SamplePosition(transform.position, out hit, Mathf.Infinity, NavMesh.AllAreas)){
			agent.Warp (hit.position);
		}
	}

	void Update () {

		//If we can see the player in our line of sight, or the player is super close to us, chase it
		if (CanSeeTarget (player.transform.position) || TargetIsInRange(player.transform.position,lineOfSightThreshold/2f)) {	//If we can see the player
			canSeePlayer = true;
			SetTarget (player);//Change our target to the player.
			if (InRange2 ()) {
				anim.SetBool("attack", true);
				anim.Play ("Attack2");
				if(!AnimatorIsPlaying("Attack2"))
				{
					enemyAttack.attack (player);
					anim.SetBool ("attack", false);
				}
			}
		} else {
			canSeePlayer = false;
			anim.SetBool("attack", false);
			//anim.Play ("Walk");
			//Only reset the target to the main goal if IT ISN'T ALREADY the current target. Otherwise we will be calculating paths every frame and that's really slow.
			if (agent.destination != target.transform.position) {
				SetTarget (target);
				//anim.Play ("Idle");

			}
		}

		//IF all of that is true, BUT we are close to our target, attack the target instead. (don't care about the player)
		if(TargetIsInRange(target.transform.position, lineOfSightThreshold)){
			SetTarget (target);
		}

		//If our target is the main goal and our path is complete? Attack the tower and then go away.
		if (InRange()) {
			
			anim.SetBool ("jump", true);
			anim.Play ("Attack3");

			//WORKS ONLY THE FIRST TIME
			if(!AnimatorIsPlaying("Attack3"))
				{
					enemyAttack.attack (target);
					anim.SetBool ("jump", false);
				}
		} else {
			anim.SetBool("jump", false);
		}

	}

    public bool isBeingAttacked()
    {
        return beingAttacked;
    }

    public void setBeingAttacked(bool attacked)
    {
        beingAttacked = attacked;
    }



    //HELPER FUNCTIONS ---- Used for tracking player, and checking for player detection.

    //Simple helper function to set the target of the agent.
    void SetTarget(GameObject _target){
		currentTarget = _target;
		agent.SetDestination (currentTarget.transform.position);
	}

	//Check to see if a target is in range of us
	public bool TargetIsInRange(Vector3 target, float distance){
		if(Vector3.SqrMagnitude(target-transform.position) <= distance){
			return true;
		}
		else{
			return false;
		}
	}

	//Check to see if the target is within range, and within our field of view.
	public bool CanSeeTarget(Vector3 _target){
		//If we are in range
		if(TargetIsInRange(_target,lineOfSightThreshold)){
			//Get the vector between us
			Vector3 targetDir = _target - transform.position;
			//Find that angle between us
			float angleToTarget = (Vector3.Angle(targetDir, transform.forward));

			//If that angle is in our FOV
			if (angleToTarget >= fovLeft && angleToTarget <= fovRight)
				return true;
			else{
				return false;
			}
		}else{
			//If we are not in range
			return false;
		}
	}

	//Stops the AI Route.
	void StopRoute(){
		agent.ResetPath ();
	}

	//Checks to see if two things are in range of 100
	public bool InRange()
	{
		if (Vector3.SqrMagnitude (transform.position - target.transform.position) <= 25) 
		{
			//Debug.Log (Vector3.SqrMagnitude (enemy - transform.position));
			//Debug.Log ("IN RANGE");
			return true;
		}
		else {
			return false;
		}
	}
	public bool InRange2()
	{
		if (Vector3.SqrMagnitude (transform.position - player.transform.position) <= 2) 
		{
			//Debug.Log (Vector3.SqrMagnitude (enemy - transform.position));
			//Debug.Log ("IN RANGE");
			return true;
		}
		else {
			return false;
		}
	}

	//Checks to see if an animation is playing
	bool AnimatorIsPlaying(){
		return anim.GetCurrentAnimatorStateInfo(0).length >
			anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
	}
	//Specific animation
	bool AnimatorIsPlaying(string stateName){
		return AnimatorIsPlaying () && anim.GetCurrentAnimatorStateInfo (0).IsName (stateName);
	}
}
