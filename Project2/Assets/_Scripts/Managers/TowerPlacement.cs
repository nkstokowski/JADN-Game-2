using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour {

    // This is the object to clone when placing a tower
    // Currently only used when debugging
    public GameObject towerObjectPrefab;

    public float sensativity;
    public float spawnHeight = 28.998f;

    // Placing state. Use this to disable any action that shouldn't happen while placing
    public bool placing = false;

    // The internal reference to the object being placed
    private GameObject towerPlacingInstance;
    private BaseTrap trapEffect;
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (placing)
        {
            Vector3 newPosition = UpdateInstancePosition(towerPlacingInstance);

            if (Input.GetButtonDown("Fire2"))
            {
                placing = false;
                Destroy(towerPlacingInstance);
            }
            else if (Input.GetButtonDown("Fire1") && canBePlaced(newPosition))
            {
                placing = false;
                trapEffect.active = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.T))
        {
            startPlacing(towerObjectPrefab);
        }
	}

    // Begin the placing process
    void startPlacing(GameObject tower)
    {
        // Only place one thing at a time
        if (!placing)
        {
            // Instantiate the new tower
            towerPlacingInstance = Instantiate(tower, Vector3.zero, Quaternion.identity);

            Vector3 newPosition = UpdateInstancePosition(towerPlacingInstance);
            // Set it to inactive
            trapEffect = towerPlacingInstance.GetComponent<BaseTrap>();
            if (trapEffect)
            {
                trapEffect.active = false;
            }

            // Set placing status
            placing = true;
        }
    }


    // Returns true if tower is in a placable location
    bool canBePlaced(Vector3 position)
    {
        return true;
    }

    Vector3 UpdateInstancePosition(GameObject obj){
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.transform.position.y - obj.transform.position.y;
        Vector3 newMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        newMousePos.y = spawnHeight;
        obj.transform.position = newMousePos;
        return newMousePos;
    }
}
