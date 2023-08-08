using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Revive : MonoBehaviour
{
    [SerializeField] private Button Revivebtn;
    [SerializeField] private Button Choosebtn;
    [SerializeField] private Button Exitbtn;
    [SerializeField] private float timer;
    [SerializeField] private Text timerUI;
    private void Awake() {
        gameObject.SetActive(false);
        Revivebtn.gameObject.SetActive(false);
        Revivebtn.onClick.AddListener(()=>{
            PlayerControl.Instance.enabled=true;
            PlayerControl.Instance.anim.SetTrigger("Alive");
            PlayerControl.Instance.m_Player.GetComponent<Creature>().currenthp=PlayerControl.Instance.m_Player.GetComponent<Creature>().maxhp;
            gameObject.SetActive(false);
        });
        Choosebtn.onClick.AddListener(()=>{SceneManager.LoadScene("Choosen");});
        Exitbtn.onClick.AddListener(()=>{Application.Quit();});
    }
    private void Start() => timer=15f;
    void Update()
    {
        timer-=Time.deltaTime;
        timerUI.text=Mathf.Round(timer).ToString();
        if(timer<=0)
        {
            timer=0;
            Revivebtn.gameObject.SetActive(true);
        }
    }
}
