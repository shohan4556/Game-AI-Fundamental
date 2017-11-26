using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollow : MonoBehaviour {

	public GameObject[] waypoints;
	private int currentWaypointID = 0;
	public float moveSpeed = 1f;
	public float rotSpeed = 1f;
	public float reachDist = 1f;

	// Use this for initialization
	void Start () {
		waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(waypoints.Length == 0) return;

		Vector3 lookAtGoal = new Vector3(waypoints[currentWaypointID].transform.position.x, this.transform.position.y, waypoints[currentWaypointID].transform.position.z);
		Vector3 direction = lookAtGoal - this.transform.position;
		// rotation 
		this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime*rotSpeed);

		if(direction.magnitude < reachDist){
			currentWaypointID++;
			if(currentWaypointID>=waypoints.Length){
				currentWaypointID = 0;
			}
		}

		this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
	}

}
