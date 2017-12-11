﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : NPCBaseFSM {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter ( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {
        base.OnStateEnter (animator, stateInfo, layerIndex);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate ( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {
        // setting up chase 
        Vector3 direction = oppnent.transform.position - NPC.transform.position;
        NPC.transform.rotation = Quaternion.Slerp (NPC.transform.rotation, Quaternion.LookRotation (direction), Time.deltaTime * rotSpeed);

        NPC.transform.Translate (0, 0, Time.deltaTime * speed);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit ( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {

    }


}
