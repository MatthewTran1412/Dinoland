using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureCancleAttack : StateMachineBehaviour
{
    [SerializeField]float atkrange;
    [SerializeField]string Type;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       Collider[] colliders = Physics.OverlapSphere(animator.transform.position,atkrange);
       if(colliders.Length!=null)
       {
            foreach (var item in colliders)
            {
                if(item.GetComponent<TargetType>())
                {
                    if(item.GetComponent<TargetType>().m_Type.ToString() == Type && item.GetComponent<Creature>().currenthp>0)
                        animator.transform.LookAt(item.transform.position);
                }
                else
                    animator.SetInteger("state",0);
            }
       }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
