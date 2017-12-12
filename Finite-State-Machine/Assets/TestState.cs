using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestState : StateMachineBehaviour {

    public int testvalue = 100;
    public GameObject testObject;

    public override void OnStateEnter ( Animator animator, AnimatorStateInfo stateInfo, int layerIndex ) {
        base.OnStateEnter (animator, stateInfo, layerIndex);

        testObject = animator.gameObject;

        Debug.Log ("Test State " + testObject);
        Debug.Log ("Test value " + testvalue);
    }


}
