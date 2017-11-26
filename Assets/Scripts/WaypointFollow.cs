using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaypointFollow : MonoBehaviour {

	public UnityStandardAssets.Utility.WaypointCircuit circuit;
	//public GameObject[] waypoints;
	private int currentWaypointID = 0;
	public float moveSpeed = 1f;
	public float rotSpeed = 1f;
	public float reachDist = 1f;

	// Use this for initialization
	void Start () {
		//waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
		//	Array.Sort(waypoints);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(circuit.Waypoints.Length == 0) return;

		Vector3 lookAtGoal = new Vector3(circuit.Waypoints[currentWaypointID].transform.position.x, this.transform.position.y, circuit.Waypoints[currentWaypointID].transform.position.z);
		Vector3 direction = lookAtGoal - this.transform.position;
		// rotation 
		this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime*rotSpeed);

		if(direction.magnitude < reachDist){
			currentWaypointID++;
			if(currentWaypointID >= circuit.Waypoints.Length){
				currentWaypointID = 0;
			}
		}

		this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
	}

}
