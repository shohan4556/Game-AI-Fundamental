using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPath_NavAgent : MonoBehaviour {

	public GameObject wpManager;
	public GameObject[] wps; 
	NavMeshAgent agent;
	private Vector3 dest;
	bool canSetDest = true;
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
		if(Input.GetMouseButton(0)){
			Vector3 point = Input.mousePosition;
			
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(point);
			if(Physics.Raycast(ray, out hit) && canSetDest){
				dest = hit.point;
				agent.SetDestination(dest);
				canSetDest = false;

				//Debug.DrawRay(Camera.main.transform.position, dest, Color.red);
			}
			
		}

		if(agent.remainingDistance <= 0.1){
			//print("Path Completed");
			canSetDest = true;
		}
		print(dest + "remaining distance : "+agent.remainingDistance);
	}

	/// <summary>
	/// Callback to draw gizmos that are pickable and always drawn.
	/// </summary>
	void OnDrawGizmos()
	{
		if(dest!=null){
			Debug.DrawLine(Camera.main.transform.position, dest, Color.black);
		 	//Debug.Log("draw ray pos : "+dest);
		}
	}

}
