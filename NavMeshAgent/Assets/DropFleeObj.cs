using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DropFleeObj : MonoBehaviour {

    public GameObject obstacle;
    private GameObject[] agents; // all agents 


	// Use this for initialization
	void Start () {
        agents = GameObject.FindGameObjectsWithTag("Agent");
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitinfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray.origin, ray.direction, out hitinfo))
            {
                //Debug.Log("Hit something : " + hitinfo.collider.name);
                //Debug.DrawLine(ray.origin, ray.direction * 10f, Color.red);
                Instantiate(obstacle, hitinfo.point, Quaternion.identity);

                foreach(GameObject item in agents) {
                    item.GetComponent<CrowdControl>().DetectNewObstacle(hitinfo.point);

                }
            }
        }
	}

    
    
    
}
