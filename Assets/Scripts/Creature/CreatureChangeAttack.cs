using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureChangeAttack : StateMachineBehaviour
{
    [SerializeField]float timer;
    [SerializeField]float cd;
    [SerializeField]float calltimer;
    [SerializeField]float callcd;
    [SerializeField]float atkrange;
    [SerializeField]string Type;
    [SerializeField]GameObject Target;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    // override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    // }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        calltimer+=Time.deltaTime;
        Collider[] colliders = Physics.OverlapSphere(animator.transform.position,atkrange);
        if(colliders.Length!=0)
        {
            foreach (var item in colliders)
            {
                if(item.GetComponent<TargetType>())
                {
                    if(item.GetComponent<TargetType>().m_Type.ToString() == Type && item.GetComponent<Creature>().currenthp>0)
                    {
                        float distance=Vector3.Distance(animator.transform.position,item.transform.position);
                        Target=!Target?item.gameObject:Vector3.Distance(animator.transform.position,Target.transform.position)>distance?item.gameObject:Target;
                    }
                }
            }
        }
        if(!Target)
            return;
        animator.transform.LookAt(Target.transform.position);
        timer+=Time.deltaTime;
        if(timer>cd)
        {
            timer=0;
            animator.SetTrigger("Attack");
        }
        if(calltimer>callcd)
        {
            calltimer=0;
            animator.SetTrigger("Call");
        }
        if(Vector3.Distance(animator.transform.position,Target.transform.position)>atkrange)
            animator.SetInteger("state",2);
        else if(Target.GetComponent<Creature>().currenthp<=0)
        {
            Target=null;
            animator.SetInteger("state",0);
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
