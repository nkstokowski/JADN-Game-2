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
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.y = spawnHeight;

            towerPlacingInstance.transform.position = mousePos;

            if (Input.GetButtonDown("Fire2"))
            {
                placing = false;
                Destroy(towerPlacingInstance);
            }
            else if (Input.GetButtonDown("Fire1") && canBePlaced(mousePos))
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
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.y = spawnHeight;
            towerPlacingInstance = Instantiate(tower, mousePos, Quaternion.identity);

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

    void moveViaMouseMovement()
    {
        Vector3 translation = Vector3.zero;
        translation.x = Input.GetAxis("Mouse X") * Time.deltaTime * sensativity;
        translation.z = Input.GetAxis("Mouse Y") * Time.deltaTime * sensativity;

        towerPlacingInstance.transform.Translate(translation);
    }
}
