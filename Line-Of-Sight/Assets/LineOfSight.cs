using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour {

    public Transform player; // target

    public float rotationSpeed = 2f;
    public float speed = 1f;
    public float visibleDist = 20f;
    public float visibleAngle = 30f;
    public float shootDist = 5f; // how close the robot has to be for shooting 

    string STATE = "Idle";

    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator> ();	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 direction = player.position - this.transform.position;
        float angle = Vector3.Angle (direction, this.transform.forward);
        //float distane = Vector3.Distance (this.transform.position, player.position);
        /*
        Debug.Log ("dir : " + direction);
        Debug.Log ("angle : " + angle);
        Debug.Log ("distance : " + distane);
        */

        // if player is in range and inforn of my eye sight  
        if(direction.magnitude < visibleDist && angle < visibleAngle) {
            //Debug.Log ("player is in front of my eyes");
            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), Time.deltaTime * rotationSpeed);

            if(direction.magnitude > shootDist) {
                if (STATE != "Running") {
                    STATE = "Running";
                    animator.SetTrigger ("isRunning");
                }
            } else {
                if (STATE != "Shooting") {
                    STATE = "Shooting";
                    animator.SetTrigger ("isShooting");
                }
            }
        } else {
            if (STATE != "Idle") {
                STATE = "Idle";
                animator.SetTrigger ("isIdle");    
            }
        }

        if(STATE == "Running") {
            transform.Translate (transform.forward * Time.deltaTime * speed);
        }

    } // end 
}
