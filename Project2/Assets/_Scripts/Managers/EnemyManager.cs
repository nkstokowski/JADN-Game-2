using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour {

	[HeaderAttribute("Customizable Variables")]
	public float spawnRadius = 50f;
	public int maxEnemyCount = 100;
	public GameObject enemyTransform;

	[HeaderAttribute("Spawner Variables")]
	public float spawnTime = 2.5f;
	public int enemiesPerSpawn = 5;

	[HeaderAttribute("Debug Variables")]
	public int currentEnemyCount;

	[HeaderAttribute("Enemy Type Variables")]
	public EnemyType[] enemyTypes;

	//Non inspector variables
	public List<GameObject> enemies = new List<GameObject>();

	void Start(){
		StartCoroutine (SpawnWave ());	//MOVE THIS TO WHEN WE ACTUALLY START THE GAME?
	}
	void Update(){
		currentEnemyCount = enemies.Count;
	}

	IEnumerator SpawnWave ()
	{
		yield return new WaitForSeconds (spawnTime);
		while (true)
		{
			for (int i = 0; i < enemiesPerSpawn; i++)
			{
				//Instantiate the enemy
				Vector3 position = Vector3.zero;
				//Set default rotation
				Quaternion spawnRotation = Quaternion.identity;

				GameObject temp = Instantiate (enemyTransform, position, spawnRotation) as GameObject;
				enemies.Add (temp);

				//Change this when we decide how to set the enemy type.
				temp.GetComponent<EnemyAttack> ().type = enemyTypes[0];

				//Calculate position around circle 
				temp.transform.position = SetEnemyPositionUntilNotColliding(spawnRadius, temp);
			}
			yield return new WaitForSeconds (spawnTime);
		}
	}

	//Recursivley sets the position of the object further and further away if the spawn position is colliding with another object.
	Vector3 SetEnemyPositionUntilNotColliding(float radius, GameObject temp){
		var angle = Random.Range(0,360);
		float x = Mathf.Cos (angle) * radius;
		float z = Mathf.Sin (angle) * radius;
		if(Physics.Raycast(new Ray(temp.transform.position,temp.transform.forward),0.05f)){
			SetEnemyPositionUntilNotColliding (radius * 1.05f, temp);
		}

		return new Vector3(x,1.0f,z);	
	}

}


public enum enemyType {
	Standard,
	Agile,
	Slow,
	Super }

[System.Serializable]
public struct EnemyType{
	public enemyType type;
	public int damage;
	public float attackRadius;

	public EnemyType(enemyType typ, int dam, float attRad){
		type = typ;
		damage = dam;
		attackRadius = attRad;
	}
}