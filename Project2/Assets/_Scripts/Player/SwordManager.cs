﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : StateMachineBehaviour
{

    public float startDamagePercent = 0.3f;
    public float stopDamagePercent = 0.5f;
    private GameObject swordObject;
    private BoxCollider swordCollider;
    private TrailRenderer swordTrail;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Ensure we have a reference to the sword
        if (!swordObject)
        {
            swordObject = GameObject.FindWithTag("Sword");
            swordCollider = swordObject.GetComponent<BoxCollider>();
            swordTrail = swordObject.GetComponent<TrailRenderer>();
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        // set sword collider to active during the specified animation time
        bool active = (stateInfo.normalizedTime >= startDamagePercent && stateInfo.normalizedTime < stopDamagePercent);
        swordCollider.enabled = active;
        swordTrail.enabled = active;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
