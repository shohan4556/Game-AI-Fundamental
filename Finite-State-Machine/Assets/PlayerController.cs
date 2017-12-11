using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float Hor = Input.GetAxisRaw ("Horizontal");
        float Vert = Input.GetAxisRaw ("Vertical");

        Vector3 input = new Vector3 (-Hor * speed, 0, -Vert * speed);
        transform.Translate (input * Time.deltaTime);


	}
}
