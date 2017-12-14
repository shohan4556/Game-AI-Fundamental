using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : NPCBaseFSM {
 
    public GameObject[] waypoints;
    int currentWaypoint;

    void Awake() {
        waypoints = GameObject.FindGameObjectsWithTag ("waypoint");
    }

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter (animator, stateInfo, layerIndex);

         agent.speed = 4f;
         agent.stoppingDistance = 2f;

        //speed = 4f;
        //rotSpeed = 1.5f;

       currentWaypoint = 0;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (waypoints.Length == 0)
            return;

        //Debug.Log (Vector3.Distance (NPC.transform.position, waypoints[currentWaypoint].transform.position));
        //// make sure the npc loop through all the waypoints 
        //if(Vector3.Distance(NPC.transform.position, waypoints[currentWaypoint].transform.position) < accuracy) {
        //    currentWaypoint++;
        //    if(currentWaypoint >= waypoints.Length) {
        //        currentWaypoint = 0;
        //    }
        //}

        if (agent.remainingDistance < accuracy) {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Length) {
                currentWaypoint = 0;
            }
        }

        // navmesh agent 
        agent.SetDestination (waypoints[currentWaypoint].transform.position);

        // rotate toward target 

        /*agent without navmesh agent*/
        //Vector3 dir = waypoints[currentWaypoint].transform.position - NPC.transform.position;

        //NPC.transform.rotation = Quaternion.Slerp (NPC.transform.rotation, Quaternion.LookRotation (dir), Time.deltaTime * rotSpeed);
        //dir.y = NPC.transform.position.y;

        ////// move towards 
        ////// only forward axis
        //NPC.transform.Translate (0, 0, Time.deltaTime * speed);
       // Debug.Log (currentWaypoint);
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}


}
