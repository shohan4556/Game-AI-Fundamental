using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {

    public FlockManager flockManager;
    float speed;

	// Use this for initialization
	void Start () {
        speed = Random.Range (flockManager.minSpeed, flockManager.maxSpeed);

	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate (transform.forward * speed * Time.deltaTime);

	}
}
