using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class PanelManager : MonoBehaviour
{
    [SerializeField]private Slider MusicSlider;
    [SerializeField]private Slider SFXSlider;
    [SerializeField]private Button CloseBtn;
    [SerializeField]private GameObject Panel;
    [SerializeField]private AudioMixer m_Mixer;
    [SerializeField]protected Toggle[] screentoggles;
    private void Awake() {
        CloseBtn.onClick.AddListener(()=>{
            Panel.SetActive(false);
            SaveSetting();
        });
        // foreach (Toggle item in screentoggles)
        // {
        //     item.onValueChanged.AddListener((t)=>OnToggleValueChanged(item,t));
        // }
    }
    private void Start() {
        if(!PlayerPrefs.HasKey("musicvolume")) PlayerPrefs.SetFloat("musicvolume",1);
        if(!PlayerPrefs.HasKey("SFXvolume")) PlayerPrefs.SetFloat("SFXvolume",1);
        LoadSetting();
        // for(int i = 0; i < screentoggles.Length; i++)
        //     screentoggles[i].isOn=false;
        // screentoggles[1].isOn=true;
    }
    private void Update() {
        ChangeVolume();
    }
    public void ChangeVolume()
    {
        m_Mixer.SetFloat("Music",Mathf.Log10(MusicSlider.value)*20);
        m_Mixer.SetFloat("SFX",Mathf.Log10(SFXSlider.value)*20);
        SaveSetting();
    }
    private void LoadSetting(){
        MusicSlider.value=PlayerPrefs.GetFloat("musicvolume");
        SFXSlider.value=PlayerPrefs.GetFloat("SFXvolume");
    }
    private void SaveSetting(){
        PlayerPrefs.SetFloat("musicvolume",MusicSlider.value);
        PlayerPrefs.SetFloat("SFXvolume",SFXSlider.value);
    }
    private void OnToggleValueChanged(Toggle toggle, bool newValue)
    {
        if (newValue)
        {
            for (int i = 0; i < screentoggles.Length; i++)
            {
                if (screentoggles[i] != toggle)
                    screentoggles[i].isOn = false;
            }
            Screen.SetResolution(int.Parse(toggle.name.Substring(0,toggle.name.IndexOf('x'))),int.Parse(toggle.name.Substring(toggle.name.LastIndexOf('x')+1)),false);
        }
    }
}
