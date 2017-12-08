using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrowdControl : MonoBehaviour {

	GameObject[] goalLocations;
	NavMeshAgent agent;

     float detectRadius = 10f;
    /*
    so far radius return an invalid path
    */
     float fleeRadius = 2f; // how far you want to run your agent 

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

   
    void ResetAgent()
    {
        // define agent speed 
        // define agent angular speed 

        // reset the agent path 
        agent.ResetPath();
    }


    public void DetectNewObstacle(Vector3 obstaclePos) {
        //print ("method get called");

        //print (Vector3.Distance (obstaclePos, transform.position));

        if(Vector3.Distance(obstaclePos, this.transform.position) < detectRadius) {
            print ("flee obj");

            Vector3 fleeDirection = (transform.position - obstaclePos).normalized;
            Vector3 newGoal = this.transform.position + fleeDirection * fleeRadius;
            
            // creating a path must make sure the environment is baked 
            NavMeshPath path = new NavMeshPath ();
            agent.CalculatePath (newGoal, path);

            if (path.status == NavMeshPathStatus.PathInvalid) {
                Debug.Log ("Invalid Path");  
            }
            // path is not invalid then flee mother fucker 
            if(path.status != NavMeshPathStatus.PathInvalid) {
                agent.SetDestination (path.corners[path.corners.Length - 1]);
                agent.speed = 20f;
                agent.angularSpeed = 500f;

                print ("fleeing");

            }
        }
    }

}
