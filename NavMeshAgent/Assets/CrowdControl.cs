using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrowdControl : MonoBehaviour {

	GameObject[] goalLocations;
	NavMeshAgent agent;
	// Use this for initialization
	void Start () {
		goalLocations = GameObject.FindGameObjectsWithTag("Goal");
		agent = GetComponent<NavMeshAgent>();
		Vector3 rndDest = goalLocations[Random.Range(0, goalLocations.Length)].transform.position;
		agent.SetDestination(rndDest);
	}
	
	// Update is called once per frame
	void Update () {
		if(agent.remainingDistance < 1){
			Vector3 rndDest = goalLocations[Random.Range(0, goalLocations.Length)].transform.position;
			agent.SetDestination(rndDest);
		}
	}
}
