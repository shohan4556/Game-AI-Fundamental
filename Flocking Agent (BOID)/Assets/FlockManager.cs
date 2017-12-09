using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
///flocking rules 
     
       (cohesion)
    1. move toward average position of the group 
        - sum of all positions / total numbers in the group
           - move them toward the average position 
        
        (alignment)
    2. align with the average heading of the group
        - sum of all heading / total numbers in the group 
         - move toward the average alignment 
          - if required avoid neighbour 
       
        (separation)  
    3. avoid crowding other group members 
        - new heading = group heading + avoid heading + group position (center position of the group)
*/

public class FlockManager : MonoBehaviour {

    public GameObject fishPrefab;
    public int numOfFish = 30;
    public GameObject[] AllFishes;
    public Vector3 swimLimits = new Vector3 (5, 5, 5);

    [Space(15)]

    [Header ("Fish Setting")]
    [Range (0.1f, 5f)]
    public float minSpeed;

    [Range (0.1f, 20f)]
    public float maxSpeed;

    [Range (1f, 20f)]
    public float neighbourDistance;

    [Range (0.1f, 20f)]
    public float rotationSpeed;

	// Use this for initialization
	void Start () {
        AllFishes = new GameObject [numOfFish];
        GameObject fishParent = new GameObject ("All Fishes");

        for(int i = 0; i < numOfFish; i++) {
            var randX = Random.Range (-swimLimits.x, swimLimits.x);
            var randY = Random.Range (-swimLimits.y, swimLimits.y);
            var randZ = Random.Range (-swimLimits.z, swimLimits.z);

            var pos = transform.position + new Vector3 (randX, randY, randZ);
            AllFishes[i] = Instantiate (fishPrefab, pos, Quaternion.identity);
            AllFishes[i].transform.parent = fishParent.transform;

            // set the reference
            AllFishes[i].GetComponent<Flock> ().flockManager = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
