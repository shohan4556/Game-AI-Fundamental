using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCol : MonoBehaviour {

    public float radius = 1f;
    public float maxDist = 0.5f;
    public LayerMask layermask;
    public Transform centerPos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hitinfo;
        if(Physics.SphereCast(centerPos.position, radius, transform.forward, out hitinfo, maxDist, layermask)) {
            Debug.Log ("Get hit " + hitinfo.distance);

        }
	}

    void OnDrawGizmos ( ) {
    
        Gizmos.DrawWireSphere (centerPos.position, radius);
    }
}
