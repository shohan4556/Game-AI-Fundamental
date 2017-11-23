using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLocal : MonoBehaviour {

	public Transform target;
	public float speed = 1f;
	public float accuracy = 1f;
	public float rotSpeed = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 lookAtTarget = new Vector3(target.position.x, this.transform.position.y, target.position.z);
		//get direction 
		Vector3 direction = lookAtTarget - transform.position;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime*rotSpeed);

		if(Vector3.Distance(transform.position, lookAtTarget)>accuracy){
			transform.Translate(direction*speed*Time.deltaTime);
		}

		//transform.LookAt(lookAtTarget);
	}
}
