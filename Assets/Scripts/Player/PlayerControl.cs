using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class PlayerControl : MonoBehaviour
{
    private static PlayerControl instance;
    public static PlayerControl Instance{get=>instance;}

    private Camera cam;
    public GameObject m_Player;
    public Transform m_Pointer;

    public NavMeshAgent m_agent;
    public Animator anim;
    public GameObject Target{get;private set;}
    public float AtkRange {get; private set;}
    
    private void Awake() {
        if(instance!=null)
            Debug.LogError("More than 1 Player Control");
        instance=this;
    }
    // Start is called before the first frame update
    private void OnEnable() {
        GameManager.Instance.e_PlayerControl+=GetPos;
        GameManager.Instance.e_PlayerControl+=PlayerMovement;
    }
    private void OnDisable() {
        GameManager.Instance.e_PlayerControl-=GetPos;
        GameManager.Instance.e_PlayerControl-=PlayerMovement;
    }
    void Start()
    {
        cam=Camera.main;
        AtkRange=5;
    }

    // Update is called once per frame
    private void GetPos()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Input.GetMouseButton(0))
        {
            if(Physics.Raycast(ray, out RaycastHit raycastHit,float.MaxValue))
            {
                Debug.Log(raycastHit.collider.GetComponent<TargetType>());
                Target=raycastHit.collider.GetComponent<TargetType>()!=null?raycastHit.collider.gameObject:null;
                m_Pointer.position=raycastHit.collider.GetComponent<TargetType>()==null?raycastHit.point:m_Pointer.position;
                m_Pointer.position=raycastHit.collider.GetComponent<TargetType>()!=null&&raycastHit.collider.GetComponent<TargetType>().m_Type==TargetType.Type.Water?raycastHit.point:m_Pointer.position;
            }
        }
        if(Input.GetMouseButtonUp(1))
            m_agent.speed=3.5f;
        else if(Input.GetMouseButtonDown(1))
            m_agent.speed=6f;
    }
    private void CheckCreature(GameObject Target)
    {
        if(m_Player.GetComponent<Animation>().currentstate!=state.idle)
            return;
        float distance = Vector3.Distance(m_Player.transform.position,Target.GetComponent<TargetType>().m_Type!=TargetType.Type.Water?Target.transform.position:m_Pointer.transform.position);
        if(m_Player.GetComponent<TargetType>().m_Type==TargetType.Type.Carnivore)
        {
            switch (Target.GetComponent<TargetType>().m_Type)
            {
                case TargetType.Type.Carnivore:
                    if(Target.GetComponent<Creature>().currenthp<=0)
                    {
                        m_Pointer.position=Target.transform.position;
                        Target=null;
                    }
                    else
                    {
                        if(distance<=AtkRange)
                        {
                            anim.SetTrigger("NorAtk");
                            Target.GetComponent<Creature>().m_Slider.gameObject.SetActive(true);
                        }
                    }
                    break;
                case TargetType.Type.Herbivore:
                    if(Target.GetComponent<Creature>().currenthp<=0)
                    {
                        m_Pointer.position=Target.transform.position;
                        Target=null;
                    }
                    else
                    {
                        if(distance<=AtkRange)
                        {
                            anim.SetTrigger("NorAtk");
                            Target.GetComponent<Creature>().m_Slider.gameObject.SetActive(true);
                        }
                    }
                    break;
                case TargetType.Type.Water:
                    if(distance<=AtkRange)
                    {
                        m_Player.GetComponent<Creature>().currenthp+=5;
                        m_Player.GetComponent<Creature>().currentwater+=10;
                        anim.SetTrigger("Drink");
                        Target=null;
                    }
                    break;
                case TargetType.Type.Meat:
                    if(distance<=AtkRange)
                    {
                        m_Player.GetComponent<Creature>().currenthp+=10;
                        m_Player.GetComponent<Creature>().currentfood+=10;
                        Destroy(Target,2);
                        anim.SetTrigger("Eat");
                    }
                    break;
            }
        }
        else if(m_Player.GetComponent<TargetType>().m_Type==TargetType.Type.Herbivore)
        {
            switch (Target.GetComponent<TargetType>().m_Type)
            {
                case TargetType.Type.Carnivore:
                    if(Target.GetComponent<Creature>().currenthp<=0)
                    {
                        m_Pointer.position=Target.transform.position;
                        Target=null;
                    }
                    else
                    {
                        if(distance<=AtkRange)
                        {
                            anim.SetTrigger("NorAtk");
                            Target.GetComponent<Creature>().m_Slider.gameObject.SetActive(true);
                        }
                    }
                    break;
                case TargetType.Type.Water:
                    if(distance<=AtkRange)
                    {
                        m_Player.GetComponent<Creature>().currenthp+=5;
                        m_Player.GetComponent<Creature>().currentwater+=10;
                        anim.SetTrigger("Drink");
                        Target=null;
                    }
                    break;
                case TargetType.Type.Herb:
                    if(distance<=AtkRange)
                    {
                        m_Player.GetComponent<Creature>().currenthp+=10;
                        m_Player.GetComponent<Creature>().currentfood+=10;
                        Destroy(Target,2);
                        anim.SetTrigger("Eat");
                    }
                    break;
            }
        }
    }
    private void PlayerMovement()
    {
        if(Target!=null)
        {
            m_agent.SetDestination(Target.GetComponent<TargetType>().m_Type==TargetType.Type.Water?m_Pointer.position:Target.transform.position);
            m_agent.stoppingDistance=AtkRange;
            m_Player.transform.LookAt(Target.GetComponent<TargetType>().m_Type==TargetType.Type.Water?m_Pointer.position:Target.transform.position);
            CheckCreature(Target);
        }
        else
            m_agent.SetDestination(m_Pointer.position); 
    }
}
