using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugRays : MonoBehaviour {

    public float range = 50f;
    public Transform end;

    void Start () {
		
	}
	

	void Update () {
        Debug.DrawRay(end.position, end.forward * range, Color.green);
		
	}
}
