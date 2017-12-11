using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBaseFSM : StateMachineBehaviour {

    public GameObject NPC;
    public GameObject oppnent; // player

    public float speed = 2f;
    public float rotSpeed = 1f;
    public float accuracy = 3f;

    // this is my fucking base class 
    public override void OnStateEnter ( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {
        base.OnStateEnter (animator, stateInfo, layerIndex);

        NPC = animator.gameObject;
        oppnent = NPC.GetComponent<EnemyAI> ().GetPlayer ();
    }

}
