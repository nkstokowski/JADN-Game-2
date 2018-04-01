using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TowerPlacement : MonoBehaviour {

    // This is the object to clone when placing a tower
    // Currently only used when debugging
    public GameObject towerObjectPrefab;
    public GameObject shopCanvas;

    [HeaderAttribute("Placement Variables")]
    public float spawnHeight = 28.998f;
    public float samplePositionRange = 0.1f;
    public Color goodLocationColor;
    public Color badLocationColor;

    // Placing state. Use this to disable any action that shouldn't happen while placing
    public bool placing = false;

    // The internal reference to the object being placed
    private GameObject towerPlacingInstance;
    private BaseTrap trapEffect;
    private Renderer towerRenderer;
    public Material[] standardMats;
    private Material[] placementMats;
    private bool placementState = true;
	AudioSource[] sounds;
	AudioSource tower;


    // Use this for initialization
    void Start () {
		sounds = gameObject.GetComponents<AudioSource> ();
		tower = sounds [1];
	}
	
	// Update is called once per frame
	void Update () {

        if (placing)
        {
            shopCanvas.SetActive(false);
            Vector3 newPosition = UpdateInstancePosition(towerPlacingInstance);
            placementState = canBePlaced(newPosition);
            setPlacementMats();

            if (Input.GetButtonDown("Fire2"))
            {
                placing = false;
                Destroy(towerPlacingInstance);
                shopCanvas.SetActive(true);
            }
            else if (Input.GetButtonDown("Fire1") && placementState)
            {
                placing = false;
                trapEffect.setActive(true);

                // Put back the old materials
                towerRenderer.materials = standardMats;
				tower.Play ();
                shopCanvas.SetActive(true);
            }
        }
        else if (Input.GetKeyUp(KeyCode.T))
        {
            startPlacing(towerObjectPrefab);
        }
	}

    // Begin the placing process
    public void startPlacing(GameObject tower)
    {
        // Only place one thing at a time
        if (!placing)
        {
            // Instantiate the new tower
            towerPlacingInstance = Instantiate(tower, Vector3.zero, Quaternion.identity);

            Vector3 newPosition = UpdateInstancePosition(towerPlacingInstance);
            // Set it to inactive
            trapEffect = towerPlacingInstance.GetComponent<BaseTrap>();
            trapEffect.setActive(true);

            // Get the renderer, save its current materials, then create a cloned array of materials for editing
            towerRenderer = towerPlacingInstance.GetComponent<Renderer>();
            standardMats = towerRenderer.materials;
            cloneMats();
            setPlacementMats();

            // Set placing status
            placing = true;
        }
    }


    // Returns true if tower is in a placable location
    bool canBePlaced(Vector3 position)
    {
        NavMeshHit hit;
        return (NavMesh.SamplePosition(position, out hit, samplePositionRange, NavMesh.AllAreas));
    }

    Vector3 UpdateInstancePosition(GameObject obj){
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.transform.position.y - obj.transform.position.y;
        Vector3 newMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        newMousePos.y = spawnHeight;
        obj.transform.position = newMousePos;
        return newMousePos;
    }


    // Update the placement materials array to the correct color
    void setPlacementMats()
    {
        Color colorToUse = (placementState) ? goodLocationColor : badLocationColor;
        for(int i=0; i < placementMats.Length; i++)
        {
            placementMats[i].color = colorToUse;
        }
        towerRenderer.materials = placementMats;
    }

    // Create the placement materials array by cloning the standard materials array
    void cloneMats()
    {
        placementMats = new Material[standardMats.Length];

        for (int i = 0; i < standardMats.Length; i++)
        {
            placementMats[i] = new Material(standardMats[i]);
        }
    }
}
