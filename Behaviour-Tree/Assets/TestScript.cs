using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class TestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    [Task]
    void outSideScript ( ) {
        Debug.Log ("out side of script");
        if (Task.isInspected) {
            Task.current.Succeed ();
        }
    }
}
