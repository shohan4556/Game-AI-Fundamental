using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {

    public FlockManager flockManager;
    public float speed;

    private bool turning = false;

	// Use this for initialization
	void Start () {
        speed = UnityEngine.Random.Range (flockManager.minSpeed, flockManager.maxSpeed);
        //Debug.Log (this.gameObject.name + " : " + speed);
	}

	
	// Update is called once per frame
	void Update () {
       
        // create a bounding box
        Bounds bound = new Bounds (flockManager.transform.position, flockManager.swimLimits * 2f);
        RaycastHit hitinfo = new RaycastHit();
        Vector3 dir = Vector3.zero;

        // if not inside the box then turn otherwise no need to turn 
        // turning = (!bound.Contains (transform.position)) ? true : false;
        if (!bound.Contains (transform.position)) {
            turning = true;
        } else {
            turning = false;
        }

        // if a fish out of bound then take it back to the center position 
        if (turning) {
            dir = flockManager.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (dir), Time.deltaTime * flockManager.rotationSpeed);
        }
        else if(Physics.SphereCast (transform.position, flockManager.radius, transform.forward, out hitinfo, flockManager.maxDist, flockManager.layermask)) {

            Debug.DrawRay (transform.position, transform.forward * flockManager.maxDist, Color.red);
            //Debug.DrawRay (transform.position, Vector3.down * 2f, Color.red);
            // Physics.Raycast (transform.position, transform.forward * 10f, out hitinfo)

            turning = true;
            dir = Vector3.Reflect (this.transform.forward, hitinfo.normal);
            transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (dir), Time.deltaTime * flockManager.rotationSpeed);

        } 
        else {
            turning = false;
        }

        if (turning) {
            Debug.Log ("turning");

        } else {
            if (UnityEngine.Random.Range (0, 100) < 10) {
                speed = UnityEngine.Random.Range (flockManager.minSpeed, flockManager.maxSpeed);
            }

            if (UnityEngine.Random.Range (0, 100) < 20) {
                ApplyRules ();
                Debug.Log ("Rule applied");
            }
        }   

        // velocity 
        transform.Translate (0,0, Time.deltaTime * speed);
    }

    void ApplyRules ( ) {
        GameObject[] allfishes;
        allfishes = flockManager.AllFishes;

        Vector3 centerPos = Vector3.zero; // average center position
        Vector3 avoidDir = Vector3.zero;
        float gSpeed = 0.01f; // global speed 
        float neighbour_dist;
        float group_size = 0; // how many fishes are in the group 

        foreach (GameObject fish in allfishes) {
            if(fish != this.gameObject) {
                neighbour_dist = Vector3.Distance (fish.transform.position, this.transform.position);
                if (neighbour_dist <= flockManager.neighbourDistance) {

                    centerPos += fish.transform.position;
                    group_size++;

                    // how close we allow another fish if too close then avoid 
                    if(neighbour_dist < 1f) {
                        avoidDir = avoidDir + (this.transform.position - fish.transform.position);
                    }

                    Flock anohterFlock = fish.GetComponent<Flock> ();
                    gSpeed = gSpeed + anohterFlock.speed; // get all the fishes speed
                }
            }
        } // end loop

        // found some neighboues 
        if(group_size > 0) {
            // calculate the average position and add the goal position
            centerPos = centerPos / group_size + (flockManager.goalPos - this.transform.position);
            // calculate average speed 
            float tmp = ((float)Math.Round(gSpeed,2) / group_size);

            tmp = (float)Math.Round (tmp, 2);
           // Debug.Log ("global speed : " + gSpeed + " group size: " + group_size + " Speed :" + tmp);

            speed = tmp;
            
            // travel direction 
            Vector3 direction = (centerPos + avoidDir) - transform.position;

            if (direction != Vector3.zero) {
                transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), Time.deltaTime * flockManager.rotationSpeed);
            }
        }

    } // end 


    void OnDrawGizmos() {
        //Debug.DrawRay (transform.position, transform.forward * 5f, Color.red);
        //Gizmos.color = new Color (1, 0, 0, 0.5F);
         //Gizmos.DrawWireCube (transform.position, Vector3.one);
         //Gizmos.DrawWireSphere (transform.position, flockManager.radius);
    }
}
