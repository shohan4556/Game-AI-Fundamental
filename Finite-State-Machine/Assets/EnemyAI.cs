using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

    private Animator animator; 

    public GameObject theplayer;
    public GameObject bullet;
    public Transform turret; // bullet instantiate position 

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator> ();
       
        //StartFiring ();
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance (transform.position, theplayer.transform.position);
        animator.SetFloat ("distance", distance);
	}

    private void Fire ( ) {
        GameObject go = Instantiate (bullet, turret.position, turret.transform.rotation);
        go.GetComponent<Rigidbody> ().AddForce (this.transform.forward * 1500f);
    }

    public void StartFiring ( ) {
        InvokeRepeating ("Fire", 0.5f, 0.5f);
    }

    public void StopFiring ( ) {
        CancelInvoke ("Fire");
    }

    public GameObject GetPlayer() {
        return theplayer;
    }

}
