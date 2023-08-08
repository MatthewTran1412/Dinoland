using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField] private Button Backbtn;
    [SerializeField] private Button Volumebtn;
    [SerializeField] private Button Exitbtn;
    [SerializeField] private Button Closebtn;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject m_Option;

    private void Update() =>OpenOption();
    private void Awake() {
        m_Option.SetActive(false);
        Backbtn.onClick.AddListener(()=>{
            m_Option.SetActive(false);
            Time.timeScale=1f;
        });
        Volumebtn.onClick.AddListener(()=>{
            panel.SetActive(true);});
        Exitbtn.onClick.AddListener(()=>{Application.Quit();});
        Closebtn.onClick.AddListener(()=>{
            m_Option.SetActive(false);
            Time.timeScale=1f;
        });
    }
    private void OpenOption()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale=0f;
            m_Option.SetActive(true);
        }    
    }
}
