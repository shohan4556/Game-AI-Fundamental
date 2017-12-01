using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour {

	Transform goal;
	public float speed = 5f;
	public float accuracy = 1f;
	public float rotSpeed = 2f;
	public GameObject wpManager;
	GameObject[] wps;
	GameObject currentNode;
	int currentWaypoint;
	Graph graph;

	// Use this for initialization
	void Start () {
		wps = wpManager.GetComponent<WaypointManager>().Waypoints;
		graph = wpManager.GetComponent<WaypointManager>().graph;
		currentNode = wps[1];
	}
	
	public void GoToHeliPad(){
		graph.AStar(currentNode, wps[1]);
		currentWaypoint = 0;
	}

	public void GoToHeliRuin(){
		graph.AStar(currentNode, wps[14]);
		currentWaypoint = 0;
	}

	// Update is called once per frame
	void LateUpdate () {
		// if you there is no path or you are at the end of the path then return 
		if(graph.getPathLength() == 0 || currentWaypoint == graph.getPathLength())
		return;

		// closest node at this point 
		currentNode = graph.getPathPoint(currentWaypoint);

		if(Vector3.Distance(graph.getPathPoint(currentWaypoint).transform.position, transform.position) < accuracy){
			currentWaypoint++;
		}

		// if you are not end of the path 
		if(currentWaypoint < graph.getPathLength()){
			goal = graph.getPathPoint(currentWaypoint).transform;
			Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
			Vector3 direction = lookAtGoal - transform.position;

			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime*rotSpeed);

			transform.Translate(0, 0, speed * Time.deltaTime);
		}


	} // end 

}
