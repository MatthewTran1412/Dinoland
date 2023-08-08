using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]private GameObject Panel;
    [SerializeField]private Button StartGameBtn;
    [SerializeField]private Button SystemBtn;
    [SerializeField]private Button ExitBtn;
    private void Awake() {
        Panel.SetActive(false);
        StartGameBtn.onClick.AddListener(()=>{SceneManager.LoadScene("Choosen");});
        SystemBtn.onClick.AddListener(()=>{Panel.SetActive(true);});
        ExitBtn.onClick.AddListener(()=>{Application.Quit();});
    }
}
