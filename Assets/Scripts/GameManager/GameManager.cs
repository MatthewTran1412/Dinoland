using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance {get=> instance;} 

    public event Action e_PlayerControl;
    public event Action e_GamePlayed;

    private Terrain m_Terrain;
    private float terrainWidth;
    private float terrainLength;
    private float xTerrainPos;
    private float zTerrainPos;
    private float randX;
    private float randZ;

    [SerializeField]private GameObject[] m_PlayerPrefs;
    [SerializeField]private GameObject[] m_Prefabs;
    [SerializeField]private GameObject[] m_herbPrefs;
    [SerializeField]private AudioClip GameSound;
    [SerializeField] private float musictimer;
    [SerializeField] private float musiccd;
    private GameObject Nature;
    [SerializeField]private int numberspawn;
    private void Awake() {
        if(instance!=null)
            Debug.LogError("Have more than 1 Manager");
        instance=this;
    }
    private void OnEnable(){
        e_GamePlayed+=RandomSpawn;
        e_GamePlayed+=PlayerSpawn;
        e_GamePlayed+=PlayMusic;
    }
    private void OnDisable() {
        e_GamePlayed-=RandomSpawn;
        e_GamePlayed-=PlayerSpawn;
        e_GamePlayed-=PlayMusic;
    }
    private void Start() {
        Application.targetFrameRate = 60;
        m_Terrain=GameObject.Find("Terrain").GetComponent<Terrain>();
        Nature=GameObject.Find("Nature");
        GetComponent<AudioSource>().PlayOneShot(GameSound);
        musictimer=Time.time;
        musiccd=GameSound.length+15;
    }
    private void PlayMusic()
    {
        if(musiccd<Time.time-musictimer)
            GetComponent<AudioSource>().PlayOneShot(GameSound);
    }
    private void Update() {
        e_GamePlayed?.Invoke();
        e_PlayerControl?.Invoke();
    }
    public void PlayerSpawn()
    {
        if(!PlayerControl.Instance.m_Player)
        {
            GetTerrainSizeAndPosition();
            do
            {
                RandomTerrainPos();
            } while (!CheckRandomPoint(new Vector3(randX,5,randZ),1f));
            string dinoname = PlayerPrefs.GetString("dino");
            for (int i = 0; i < m_PlayerPrefs.Length; i++)
            {
                if(m_PlayerPrefs[i].name.ToString().Contains(dinoname))
                {
                    PlayerControl.Instance.m_Player=Instantiate(m_PlayerPrefs[i],new Vector3(randX,5,randZ),Quaternion.identity);
                    PlayerControl.Instance.m_Player.transform.SetParent(PlayerControl.Instance.transform);
                    PlayerControl.Instance.m_agent=PlayerControl.Instance.m_Player.GetComponent<UnityEngine.AI.NavMeshAgent>();
                    PlayerControl.Instance.m_agent.speed=3.5f;
                    PlayerControl.Instance.anim=PlayerControl.Instance.m_Player.GetComponent<Animator>();
                    PlayerControl.Instance.m_Pointer.position=PlayerControl.Instance.m_Player.transform.position;
                }
            }
        }
    }
    private void RandomSpawn()
    {
        float numHerb=GameObject.FindGameObjectsWithTag("herbivore").Length;
        float numCarn=GameObject.FindGameObjectsWithTag("carnivore").Length;
        float numherb=GameObject.FindGameObjectsWithTag("herb").Length;
        // float numherb=;
        if(m_Terrain)
        {
            GetTerrainSizeAndPosition();
            if((numHerb+numCarn)<numberspawn)
            {
                for (int i = 0; i < numberspawn; i++)
                {   
                    do
                    {
                        RandomTerrainPos();
                    } while (!CheckRandomPoint(new Vector3(randX,5,randZ),1f));
                    Instantiate(m_Prefabs[UnityEngine.Random.Range(0,m_Prefabs.Length)],new Vector3(randX,5,randZ),Quaternion.identity).transform.SetParent(Nature.transform.Find("Creature").transform);
                }
            }
            if(numherb<numberspawn)
            {
                for (int i = 0; i < numberspawn; i++)
                {   
                    RandomTerrainPos();
                    Instantiate(m_herbPrefs[UnityEngine.Random.Range(0,m_herbPrefs.Length)],new Vector3(randX,5,randZ),Quaternion.identity).transform.SetParent(Nature.transform.Find("Herb").transform);
                }
            }
        }
    }

    private void RandomTerrainPos()
    {
        if(m_Terrain)
        {
            randX = UnityEngine.Random.Range(xTerrainPos, xTerrainPos + terrainWidth);
            randZ = UnityEngine.Random.Range(zTerrainPos, zTerrainPos + terrainLength);
        }
    }
    private void GetTerrainSizeAndPosition()
    {
        //Get terrain size
        terrainWidth = m_Terrain.terrainData.size.x;
        terrainLength = m_Terrain.terrainData.size.z;

        //Get terrain position
        xTerrainPos = m_Terrain.transform.position.x;
        zTerrainPos = m_Terrain.transform.position.z;
    }
    private bool CheckRandomPoint(Vector3 center, float range)
    {
        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(center, out hit, 1.0f, UnityEngine.AI.NavMesh.AllAreas))
            return true;
        return false;
    }
}
