using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Panda;

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

    /*---------------------Panda BT--------------------------*/
    [Task]
    public void PickRandomDestination ( ) {
        Vector3 dest = new Vector3 (Random.Range (-100, 100), 0, Random.Range (-100, 100));
        agent.SetDestination (dest);
        // make sure the task is succeed
        Task.current.Succeed ();

    }

    [Task]
    public void MoveToRandomDestination ( ) {
        //debug task 
        if (Task.isInspected) {
            Task.current.debugInfo = string.Format ("t={0:0.00}", Time.time);
        }

        if(agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending) {
            Task.current.Succeed ();
        }
    }


}
