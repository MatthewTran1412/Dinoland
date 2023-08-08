using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject m_Player;

    // Update is called once per frame
    void Update()=>UpdateCam();
    private void UpdateCam()
    {
        if(GameObject.Find("PlayerManager").transform.childCount<2)
            return;
        m_Player=GameObject.Find("PlayerManager").transform.GetChild(1).gameObject;
        Vector3 camPos = new Vector3(m_Player.transform.position.x,transform.position.y,m_Player.transform.position.z-30);
        transform.position=camPos;
        transform.LookAt(m_Player.transform.position);
    }
}
