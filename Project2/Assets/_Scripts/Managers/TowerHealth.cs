﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerHealth : MonoBehaviour {

	public int health = 100;
	public GameObject effect;
	public float range = 10f;

	public GameObject manager;

	public List<GameObject> enemyList = new List<GameObject>();

	void Start()
	{
		InvokeRepeating ("Pulse", 0, 15);
	}
	void Update()
	{
		
		//Debug.Log (enemies.Count);
	}

	//Prevents duplication of enemies if they are being added to a list
	public void CheckEnemy()
	{
		GameObject[]  targets = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject go in targets) 
		{
			if (!enemyList.Contains (go)) 
			{
				enemyList.Add (go);
			}
		}
	}

	//Instantiates the Effect as well as delays the objects destruction
	public void Pulse()
	{
		//CheckEnemy ();
		//ADD A SECOND INVOKE
		Instantiate (effect, new Vector3(transform.position.x, transform.position.y + 8, transform.position.z), Quaternion.identity);
		//Invoke ("RemoveAllEnemies", 8);
		Invoke ("DestroyExcess", 11);
		//Debug.Log (health);
		//DestroyAllEnemies();
	}

	//Checks to see if two things are in range of 100
	public bool InRange(Vector3 enemy)
	{
		if (Vector3.SqrMagnitude (enemy - transform.position) <= 30) 
		{
			//Debug.Log (Vector3.SqrMagnitude (enemy - transform.position));
			//Debug.Log ("IN RANGE");
			return true;
		}
		else {
			return false;
		}
	}

	//Removes Enemies from a list 
	public void RemoveAllEnemies()
	{
		if (enemyList.Count == 0) {
		} else {
			for (int i = 0; i < enemyList.Count + 1; i++) {
				//Debug.Log (enemyList [0]);
				if (InRange (enemyList[i].transform.position)) 
				{
					enemyList.Remove (enemyList[i]);
				}
			}
		}
	}

	//Destroys all objects within a certain distance
	public void DestroyExcess()
	{
		GameObject[] targets = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject o in targets) {
			if (InRange(o.transform.position)) 
			{
				health -= 1;
				Destroy (o);
			}
		}
	}
	public void TakeDamage(int damage){
		health -= damage;
		if(CheckForDeath()){
			PlayerPrefs.SetInt("playerScore", manager.GetComponent<ScoreManager>().playerScore);
			SceneManager.LoadScene("GameOver");
		}
		else{
			return;
		}
	}

	private bool CheckForDeath(){
		if(health <= 0){
			return true;
		}
		else{
			return false;
		}
	}


}
