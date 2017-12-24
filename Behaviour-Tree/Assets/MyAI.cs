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

    public float health = 100f;
    public float rotSpeed = 5f;

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
    public void PickDestination(float x, float z ) {
        Vector3 dest = new Vector3 (x, 0, z);
        agent.SetDestination (dest);

        Task.current.Fail ();
        //Task.current.Succeed (); /*This is not a return type you can call from anywhere in the code*/
    }

    
    [Task]
    public void PickRandomDestination ( ) {
        Vector3 dest = new Vector3 (Random.Range (-100, 100), 0, Random.Range (-100, 100));
        agent.SetDestination (dest);
        // make sure the task is succeed
        Task.current.Succeed ();

    }

    [Task]
    public void MoveToDestination ( ) {
        //debug task 
        if (Task.isInspected) {
            Task.current.debugInfo = string.Format ("t={0:0.00}", Time.time);
        }

        if(agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending) {
            Task.current.Succeed ();
        }
    }

    /*--------------------Attack BT---------------------------------*/
    [Task]
    public void TargetPlayer ( ) {
        target = player.transform.position;
        Task.current.Succeed ();
    }


    [Task]
    public void LookAtTarget ( ) {
        Vector3 dir = target - this.transform.position;
        transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (dir), Time.deltaTime * rotSpeed);

        //Debug.Log (Vector3.Angle (transform.forward, dir));

        if (Task.isInspected) {
            if(Vector3.Angle(transform.forward, dir) < 5f) {
                Task.current.Succeed ();
            }
        }
    } // end 


    [Task]
    public bool Fire ( ) {
        GameObject bullet = Instantiate (bulletPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation);
        bullet.GetComponent<Rigidbody> ().AddForce (bullet.transform.forward * 1000);
        return true;
    }

    [Task]
    bool SeenPlayer ( ) {
        Vector3 dir = player.transform.position - this.transform.position;
        RaycastHit hitinfo;
        bool seeWall = false;

        Debug.DrawRay (transform.position, dir, Color.red);

        if(Physics.Raycast(transform.position, dir, out hitinfo)) {
            if(hitinfo.collider.gameObject.tag == "wall") {
                seeWall = true;
            }
        }

        if (Task.isInspected) {
            Task.current.debugInfo = string.Format ("wall= {0} ", seeWall);
        }

        if(dir.magnitude < visibleRange && !seeWall) {
            return true;
        }

        return false;
    }

    /*------------------------------------------------------------*/
    [Task]
    bool Turn(float angle){
        Vector3 pos = transform.position - Quaternion.AngleAxis(angle, Vector3.up) * transform.forward;
        target = pos;
        return true;
    }

    [Task]
    public bool isHealthLessThan(float h){
        if(this.health < h ){
            return true;
        }
        return false;
    }
        
    /*if the player is very close to the enemy then its in danger */
    [Task]
    public bool InDange(float minDist){
        Vector3 distance = player.transform.position - this.transform.position;
        
        if(distance.magnitude < minDist)
            return true;

        return false;
    }

    [Task]
    public void RunAwayFromPlayer(){
        Vector3 runDir = transform.position - player.transform.position;
        Vector3 dest = transform.position +runDir * 2f;
        agent.SetDestination(dest);

        Task.current.Succeed();
    }

    /* destroy when life goes down*/
    [Task]
    bool Explode(){
        Destroy(healthbar.gameObject);
        Destroy(this.gameObject);
        return true;
    }

}
