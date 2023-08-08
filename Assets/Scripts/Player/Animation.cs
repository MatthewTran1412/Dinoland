using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum state
{
    idle,
    walk,
    run,
    attack,
    hurt,
    death,
}
public class Animation : MonoBehaviour
{
    // private static Animation instance;
    // public static Animation Instance{get=>instance;}

    public state currentstate=state.idle;
    [SerializeField]private float lastcount;
    [SerializeField]private float cd=10f;
    // Start is called before the first frame update
    // private void Awake() {
        // if(instance!=null)
            // Debug.LogError("More than 1 Animation");
        // instance=this;
    // }
    private void OnEnable(){
        if(GameManager.Instance)
            GameManager.Instance.e_PlayerControl+=SetAnimation;
    }
    private void OnDisable() => GameManager.Instance.e_PlayerControl-=SetAnimation;
    
    void Start()=>lastcount=Time.time;

    private void SetAnimation()
    {
        if(currentstate==state.idle && !PlayerControl.Instance.Target)
        {
            if(cd<Time.time-lastcount)
            {
                PlayerControl.Instance.anim.SetTrigger("Idle");
                lastcount=Time.time;
            }
        }
        if(PlayerControl.Instance.m_agent.velocity.magnitude>0.01f && PlayerControl.Instance.m_agent.speed>0f)
        {
            if(PlayerControl.Instance.m_agent.speed>3.5f)
                currentstate=state.run;
            else
                currentstate=state.walk;
        }
        else
            currentstate=state.idle;
        PlayerControl.Instance.anim.SetInteger("state",(int)currentstate);
    }
}
