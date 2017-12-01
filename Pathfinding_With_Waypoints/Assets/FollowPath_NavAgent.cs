using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPath_NavAgent : MonoBehaviour {

	public GameObject wpManager;
	public GameObject[] wps; 
	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		wps = wpManager.GetComponent<WaypointManager>().Waypoints;
		agent = GetComponent<NavMeshAgent>();
		
		//GoToHeli();
	}
	
	public void GoToHeli()
	{
		agent.SetDestination(wps[4].transform.position);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
