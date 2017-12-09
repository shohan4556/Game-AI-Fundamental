using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {

    public FlockManager flockManager;
    public float speed;

    void OnValidate() {
        if (speed <= 2f) {
            Debug.Log ("validate");
        }
    }

	// Use this for initialization
	void Start () {
        speed = UnityEngine.Random.Range (flockManager.minSpeed, flockManager.maxSpeed);
        //Debug.Log (this.gameObject.name + " : " + speed);
	}
	
	// Update is called once per frame
	void LateUpdate () {
        ApplyRules ();

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
            // calculate the average position 
            centerPos = centerPos / group_size;
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

}
