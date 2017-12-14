using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class MyAI : MonoBehaviour {

    public Transform player;
    public Transform bulletSpawnPos;
    public Slider healthbar;
    public GameObject bulletPrefab;

    private NavMeshAgent agent;
    public Vector3 destination;
    public Vector3 target;

    float health = 100f;
    float rotSpeed = 5f;

    public float visibleRange = 80f;
    public float shootRange = 40f;

	// Use this for initialization
	void Start () {
        agent = this.GetComponent<NavMeshAgent> ();
        agent.stoppingDistance = shootRange - 5;

        InvokeRepeating ("UpdateHealth", 1f,0.2f);    
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 healthbarPos = Camera.main.WorldToScreenPoint (transform.position);
        healthbar.transform.position = healthbarPos + new Vector3(0,40,0);
        healthbar.value = (int)health;
	}

    void UpdateHealth ( ) {
        if (health < 100)
            health++;
    }

    void OnCollisionEnter(Collision col ) {
        if(col.gameObject.tag == "bullet") {
            health -= 10;
        }
    }


}
