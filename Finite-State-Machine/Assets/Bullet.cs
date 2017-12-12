using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    	
	}
	
	void OnTriggerExit(Collider col ) {
        if(col.tag == "Player") {
            Destroy (this.gameObject);
            return;
        }
        Destroy (this.gameObject,2f);
    }
}
