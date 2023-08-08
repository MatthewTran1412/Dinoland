using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureIdle : StateMachineBehaviour
{
    [SerializeField]float Range;
    [SerializeField]float timer;
    [SerializeField]float idletimer;
    [SerializeField]float playidletimer;
    [SerializeField]float staytimer;
    [SerializeField]string Type;
    [SerializeField]float time2sleep;
    [SerializeField]float sleeptime;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    // override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    // }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Collider[] colliders = Physics.OverlapSphere(animator.transform.position,Range);
        if(colliders.Length!=0)
        {
            foreach (var item in colliders)
            {
                if(item.GetComponent<TargetType>())
                {
                    if(item.GetComponent<TargetType>().m_Type.ToString() == Type)
                        animator.SetInteger("state",2);
                    else if(item.GetComponent<CreatureAttack>()&&item.GetComponent<CreatureAttack>().isCall)
                        animator.SetInteger("state",2);
                }
            }
        }
        timer+=Time.deltaTime;
        idletimer+=Time.deltaTime;
        sleeptime+=Time.deltaTime; 
        if(idletimer>playidletimer)
        {
            idletimer=0;
            animator.SetTrigger("Idle");
        }
        else if(timer>staytimer)
        {
            timer=0;
            animator.SetInteger("state",1);
        }
        else if(sleeptime>time2sleep)
        {
            sleeptime=0;
            animator.SetBool("Sleep",true);
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
