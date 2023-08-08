using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager instance;
    public static SpawnManager Instance{get=>instance;}
    public GameObject[] m_Prefabs;
    [SerializeField]private Transform m_Spawnpoint;
    public GameObject Choosing;
    [SerializeField]private Button Previous;
    [SerializeField]private Button Next;
    [SerializeField]private Button Choose;
    [SerializeField]private Text t_name;
    private int id=0;
    private int Oldid;
    private void Awake() {
        if(instance)
            Debug.LogError("Have more than 1 spawn manager");
        instance=this;
        Next.onClick.AddListener(()=>{id=id>=m_Prefabs.Length-1?0:id+1;});
        Previous.onClick.AddListener(()=>{id=id<=0?m_Prefabs.Length-1:id-1;});
        Choose.onClick.AddListener(()=>{
            PlayerPrefs.SetString("dino",Choosing.name.ToString().Replace("(Clone)",string.Empty));
            GameObject.Find("Canvas").transform.Find("LoadingImage").gameObject.SetActive(true);
        });
    }
    private void Start() {
        Oldid=id;
        if(!Choosing)
            Choosing=Instantiate(m_Prefabs[id],m_Spawnpoint.position,m_Prefabs[id].transform.rotation);
    }
    private void Update() {
        t_name.text=Choosing.name.ToString().Replace("(Clone)",string.Empty);
        if(id!=Oldid)
        {
            Destroy(Choosing);
            Choosing=Instantiate(m_Prefabs[id],m_Spawnpoint.position,m_Prefabs[id].transform.rotation);
            Oldid=id;
        }
    }
}
