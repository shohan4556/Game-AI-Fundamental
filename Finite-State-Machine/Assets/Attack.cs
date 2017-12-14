using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : NPCBaseFSM {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter ( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {
        base.OnStateEnter (animator, stateInfo, layerIndex);

        //start attack
        agent.ResetPath ();

        NPC.GetComponent<EnemyAI> ().StartFiring ();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate ( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {

        NPC.transform.LookAt (oppnent.transform.position);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit ( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {

        NPC.GetComponent<EnemyAI> ().StopFiring ();
    }

  
}
