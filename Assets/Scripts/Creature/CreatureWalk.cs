using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureWalk : StateMachineBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] float Range;
    [SerializeField] float radius;
    [SerializeField]string Type;
    [SerializeField] private float timer;
    [SerializeField] private float starttime;
    Vector3 Pos;
    bool canwalk;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent=animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
       agent.speed=3.5f;
       canwalk=true;
       agent.isStopped = false;
       timer=7f;
    }

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
                    {
                        animator.SetInteger("state",2);
                    }
                }
            }
        }
        if(canwalk)
        {
            Vector3 randomPos = Random.insideUnitSphere * radius + animator.transform.position;
            UnityEngine.AI.NavMeshHit hit;
            UnityEngine.AI.NavMesh.SamplePosition(randomPos, out hit, radius, UnityEngine.AI.NavMesh.AllAreas);
            Pos=hit.position;
            agent.SetDestination(Pos);
            canwalk=false;
            starttime=Time.time;
        }
        if(animator.transform.position==Pos||timer<Time.time-starttime)
        {
            agent.isStopped = true; // was agent.Stop();
            agent.velocity = Vector3.zero;
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
