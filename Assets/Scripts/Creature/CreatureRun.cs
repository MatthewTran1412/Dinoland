using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CreatureRun : StateMachineBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    [SerializeField]float Range;
    // [SerializeField]float distance;
    [SerializeField]float atkrange;
    [SerializeField]string Type;
    [SerializeField]GameObject Target;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent=animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
       agent.speed=5f;
       agent.isStopped = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 myVector = new Vector3(2,0,2);
        Collider[] colliders = Physics.OverlapSphere(animator.transform.position,Range);
        if(colliders.Length!=0)
        {
            // colliders=colliders.Skip(1).ToArray();
            // Debug.Log(colliders.OrderBy(item =>Vector3.Distance(animator.transform.position,item.transform.position)));
            foreach (var item in colliders)
            {
                if(item.GetComponent<TargetType>())
                {
                    if(item.GetComponent<TargetType>().m_Type.ToString() == Type && item.GetComponent<Creature>().currenthp>0)
                    {
                        if(!Target)
                            Target=item.gameObject;
                        else
                        {
                            float distance= Vector3.Distance(animator.transform.position,item.transform.position);
                            Target=Vector3.Distance(animator.transform.position,Target.transform.position)>distance?item.gameObject:Target;
                        }
                    }        
                    else 
                        Target=item.GetComponent<CreatureAttack>()&&item.GetComponent<CreatureAttack>().isCall && animator.name.Contains("raptor")?item.gameObject:Target;
                }
            }
        }
        if(!Target)
            return;
        agent.SetDestination(Target.transform.position-myVector);
        agent.stoppingDistance = atkrange-2;
        if(Vector3.Distance(animator.transform.position,Target.transform.position)<=atkrange)
            animator.SetInteger("state",3);
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
