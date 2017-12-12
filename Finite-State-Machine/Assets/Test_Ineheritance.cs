using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Ineheritance : TestState {

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter ( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {
        base.OnStateEnter (animator, stateInfo, layerIndex);

        Debug.Log ("Child State");

        /*
        when this state runs, unity automatically pass the animator references and other param
        when this state called the base class also get called after then this child method runs 
        */
    }

}
