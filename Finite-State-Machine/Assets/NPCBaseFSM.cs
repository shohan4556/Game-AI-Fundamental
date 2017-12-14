using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBaseFSM : StateMachineBehaviour {

    public GameObject NPC;
    public GameObject oppnent; // player

    public float speed = 4f;
    public float rotSpeed = 1.5f;
    public float accuracy = 3f;
    public NavMeshAgent agent;

    // this is my fucking base class 
    public override void OnStateEnter ( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {
        base.OnStateEnter (animator, stateInfo, layerIndex);

        NPC = animator.gameObject;
        // get agent (this gameobject)
        agent = NPC.GetComponent<NavMeshAgent> ();
        // get the player 
        oppnent = NPC.GetComponent<EnemyAI> ().GetPlayer ();

        //Debug.Log ("NPC base Class :" + animator.gameObject.name);
    }

}
